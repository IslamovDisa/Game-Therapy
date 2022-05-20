using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;

public class SingIn : MonoBehaviour
{
    private FirebaseAuth _auth;
    private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

    protected Dictionary<string, FirebaseUser> UserByAuth = new Dictionary<string, FirebaseUser>();
    
    protected string PhoneNumber = "";
    protected string ReceivedCode = "";
    protected string DisplayName = "";
    
    // Whether to sign in / link or re authentication *and* fetch user profile data.
    protected readonly bool SignInAndFetchProfile = false;
    // Set the phone authentication timeout to a minute.
    private const uint PhoneAuthTimeoutMs = 60 * 1000;
    // The verification id needed along with the sent code for phone authentication.
    private string _phoneAuthVerificationId;
    
    // Flag set when a token is being fetched.  This is used to avoid printing the token
    // in IdTokenChanged() when the user presses the get token button.
    private const bool FetchingToken = false;
    private bool _uiEnabled = true;

    [Space(10)]
    [Header("UI")]
    [SerializeField] private Button _verifyPhoneNumberButton;
    [SerializeField] private Button _verifyReceivedPhoneCodeButton;
    [Space(10)]
    [SerializeField] private InputField _phoneInputField;
    [SerializeField] private InputField _codeInputField;

    public virtual void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            _dependencyStatus = task.Result;
            if (_dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " +
                               _dependencyStatus);
            }
        });
        
        _verifyPhoneNumberButton.onClick.AddListener(() =>
        {
            PhoneNumber = _phoneInputField.text;
            VerifyPhoneNumber();
        });
        
        _verifyReceivedPhoneCodeButton.onClick.AddListener(() =>
        {
            ReceivedCode = _codeInputField.text;
            VerifyReceivedPhoneCode();
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        _auth = FirebaseAuth.DefaultInstance;
        _auth.StateChanged += AuthStateChanged;
        _auth.IdTokenChanged += IdTokenChanged;

        AuthStateChanged(this, null);
    }

    private void OnDestroy()
    {
        if (_auth == null)
        {
            return;
        }
        
        _auth.StateChanged -= AuthStateChanged;
        _auth.IdTokenChanged -= IdTokenChanged;
        _auth = null;
    }

    private void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        var senderAuth = sender as FirebaseAuth;
        FirebaseUser user = null;
        if (senderAuth != null)
        {
            UserByAuth.TryGetValue(senderAuth.App.Name, out user);
        }

        if (senderAuth != _auth || senderAuth?.CurrentUser == user)
        {
            return;
        }
        
        var signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
        if (!signedIn && user != null)
        {
            Debug.Log("Signed out " + user.UserId);
        }

        user = senderAuth.CurrentUser;
        UserByAuth[senderAuth.App.Name] = user;
        if (!signedIn)
        {
            return;
        }
        
        Debug.Log("AuthStateChanged Signed in " + user.UserId);
        DisplayName = user.DisplayName ?? "";
        DisplayDetailedUserInfo(user, 1);
    }

    private void IdTokenChanged(object sender, EventArgs eventArgs)
    {
        var senderAuth = sender as FirebaseAuth;
        if (senderAuth == _auth && senderAuth?.CurrentUser != null && !FetchingToken)
        {
            senderAuth.CurrentUser.TokenAsync(false).ContinueWithOnMainThread(
                task => Debug.Log($"Token[0:8] = {task.Result.Substring(0, 8)}"));
        }
    }

    protected void VerifyPhoneNumber()
    {
        var phoneAuthProvider = PhoneAuthProvider.GetInstance(_auth);
        phoneAuthProvider.VerifyPhoneNumber(PhoneNumber, PhoneAuthTimeoutMs, null,
            (cred) =>
            {
                Debug.Log("Phone Auth, auto-verification completed");
                if (SignInAndFetchProfile)
                {
                    _auth.SignInAndRetrieveDataWithCredentialAsync(cred).ContinueWithOnMainThread(
                        HandleSignInWithSignInResult);
                }
                else
                {
                    _auth.SignInWithCredentialAsync(cred).ContinueWithOnMainThread(HandleSignInWithUser);
                }
            },
            
            (error) => { Debug.Log("Phone Auth, verification failed: " + error); },
            
            (id, token) =>
            {
                _phoneAuthVerificationId = id;
                Debug.Log("Phone Auth, code sent");
            },
            
            (id) =>
            {
                Debug.Log("Phone Auth, auto-verification timed out");
            });
    }

    protected void VerifyReceivedPhoneCode()
    {
        var phoneAuthProvider = PhoneAuthProvider.GetInstance(_auth);
        
        // receivedCode should have been input by the user.
        var cred = phoneAuthProvider.GetCredential(_phoneAuthVerificationId, ReceivedCode);
        if (SignInAndFetchProfile)
        {
            _auth.SignInAndRetrieveDataWithCredentialAsync(cred).ContinueWithOnMainThread(
                HandleSignInWithSignInResult);
        }
        else
        {
            _auth.SignInWithCredentialAsync(cred).ContinueWithOnMainThread(HandleSignInWithUser);
        }
    }

    private void HandleSignInWithSignInResult(Task<SignInResult> task)
    {
        _uiEnabled = true;
        if (LogTaskCompletion(task, "Sign-in"))
        {
            DisplaySignInResult(task.Result, 1);
        }
    }

    private void HandleSignInWithUser(Task<FirebaseUser> task)
    {
        _uiEnabled = true;
        if (LogTaskCompletion(task, "Sign-in"))
        {
            Debug.Log($"{task.Result.DisplayName} signed in");
        }
    }
    
    // Log the result of the specified task, returning true if the task
    // completed successfully, false otherwise.
    private static bool LogTaskCompletion(Task task, string operation)
    {
        var complete = false;
        if (task.IsCanceled)
        {
            Debug.Log(operation + " canceled.");
        }
        else if (task.IsFaulted)
        {
            Debug.Log(operation + " encounter an error.");
            foreach (var exception in task.Exception?.Flatten().InnerExceptions!)
            {
                var authErrorCode = "";
                if (exception is FirebaseException firebaseEx)
                {
                    authErrorCode = $"AuthError.{((AuthError)firebaseEx.ErrorCode).ToString()}: ";
                }

                Debug.Log(authErrorCode + exception.ToString());
            }
        }
        else if (task.IsCompleted)
        {
            Debug.Log(operation + " completed");
            complete = true;
        }

        return complete;
    }

    private static void DisplaySignInResult(SignInResult result, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        DisplayDetailedUserInfo(result.User, indentLevel);
        
        var metadata = result.Meta;
        if (metadata != null)
        {
            Debug.Log($"{indent}Created: {metadata.CreationTimestamp}");
            Debug.Log($"{indent}Last Sign-in: {metadata.LastSignInTimestamp}");
        }

        var info = result.Info;
        if (info == null)
        {
            return;
        }
        
        Debug.Log($"{indent} Additional User Info:");
        Debug.Log($"{indent}  User Name: {info.UserName}");
        Debug.Log($"{indent}  Provider ID: {info.ProviderId}");
        
        DisplayProfile(info.Profile, indentLevel + 1);
    }

    private static void DisplayDetailedUserInfo(FirebaseUser user, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        
        DisplayUserInfo(user, indentLevel);
        Debug.Log($"{indent}Anonymous: {user.IsAnonymous}");
        Debug.Log($"{indent}Email Verified: {user.IsEmailVerified}");
        Debug.Log($"{indent}Phone Number: {user.PhoneNumber}");
        
        var providerDataList = new List<IUserInfo>(user.ProviderData);
        var numberOfProviders = providerDataList.Count;
        
        if (numberOfProviders <= 0)
        {
            return;
        }
        
        for (var i = 0; i < numberOfProviders; ++i)
        {
            Debug.Log($"{indent}Provider Data: {i}");
            DisplayUserInfo(providerDataList[i], indentLevel + 2);
        }
    }

    private static void DisplayProfile<T>(IDictionary<T, object> profile, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        foreach (var kv in profile)
        {
            if (kv.Value is IDictionary<object, object> valueDictionary)
            {
                Debug.Log($"{indent}{kv.Key}:");
                DisplayProfile(valueDictionary, indentLevel + 1);
            }
            else
            {
                Debug.Log($"{indent}{kv.Key}: {kv.Value}");
            }
        }
    }

    private static void DisplayUserInfo(IUserInfo userInfo, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);
        var userProperties = new Dictionary<string, string>
        {
            { "Display Name", userInfo.DisplayName },
            { "Email", userInfo.Email },
            { "Photo URL", userInfo.PhotoUrl != null ? userInfo.PhotoUrl.ToString() : null },
            { "Provider ID", userInfo.ProviderId },
            { "User ID", userInfo.UserId }
        };

        foreach (var property in userProperties)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                Debug.Log($"{indent}{property.Key}: {property.Value}");
            }
        }
    }
}
