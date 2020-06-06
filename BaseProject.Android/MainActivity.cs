using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using BaseMvvmToolKIt;
using BaseProject.Services.FileSystem;
using BaseProject.Constants;

namespace BaseProject.Droid
{
    [Activity(Label = "BaseProject", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this,bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Acr.UserDialogs.UserDialogs.Init(this);

            LoadApplication(new App());

            DisplayCrashReport();

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region Error Handling
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            LogUnhandledException(newExc);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        internal static void LogUnhandledException(Exception exception)
        {
            try
            {
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); 
                var errorFilePath = System.IO.Path.Combine(libraryPath, AppConstants.ErrorFileName);
                var errorMessage = string.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}", DateTime.Now, exception.ToString());
                System.IO.File.WriteAllText(errorFilePath, errorMessage);
                // Log to Android Device Logging.
                Android.Util.Log.Error("Crash Report", errorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
                // just suppress any error logging exceptions
            }
        }

        /// <summary>
        // If there is an unhandled exception, the exception information is diplayed 
        // on screen the next time the app is started (only in debug configuration)
        /// </summary>
        //[Conditional("DEBUG")]
        private void DisplayCrashReport()
        {
            var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var errorFilePath = System.IO.Path.Combine(libraryPath, AppConstants.ErrorFileName);


            if (!System.IO.File.Exists(errorFilePath))
            {
                return;
            }

            var errorText = System.IO.File.ReadAllText(errorFilePath);
            if (string.IsNullOrEmpty(errorText))
            {
                return;
            }


            new AlertDialog.Builder(this)
                .SetPositiveButton("Clear", (sender, args) =>
                {
                    System.IO.File.Delete(errorFilePath);
                })
                .SetNegativeButton("Close", (sender, args) =>
                {
                    // When User pressed Close.
                })
                .SetMessage(errorText)
                .SetTitle("Crash Report")
                .Show();

        }
        #endregion
    }
}