using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using MonoGameExample;
using Microsoft.Xna.Framework;
using Android.Gms.Ads;
using MonoGame3D.Droid.Tools;

namespace MonoGame3D.Droid
{
    [Activity(Label = "MonoGame3D.Droid",
        MainLauncher = true,
        Icon = "@drawable/icon",
        Theme = "@style/Theme.Splash",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.Landscape,
        ConfigurationChanges = ConfigChanges.Orientation |
        ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize |
        ConfigChanges.Keyboard)]
    public class Activity1 : AndroidGameActivity
    {
        protected AdView adView;

        private static InterstitialAd interstitial;
        private const string AdMob_AdUnitID = "";
        private readonly string[] TestDevices = new string[]  { "" };  
        private LinearLayout ll;
        private FrameLayout fl;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create our OpenGL view, and display it
            var g = new Game1();


            View gameView = (View)g.Services.GetService(typeof(View));
            fl = new FrameLayout(this);
            fl.AddView(gameView);
            RefreshBannerAd();
            SetContentView(fl);
            g.Run();


        }


        // https://forums.xamarin.com/discussion/30171/admob-banner-not-refreshing-after-screen-is-turned-back-on
        protected override void OnPause()
        {
            adView.Pause();
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            adView.Resume();
        }

        protected override void OnDestroy()
        {
            adView.Destroy();
            base.OnDestroy();
        }



        public void ShowInterstitialAd()
        {
            RunOnUiThread(() =>
            {
                if (interstitial.AdListener != null)
                    interstitial.AdListener.Dispose();
                interstitial.AdListener = null;
                var intlistener = new adlistener();
                intlistener.AdLoaded += () => { if (interstitial.IsLoaded) interstitial.Show(); };
                interstitial.AdListener = intlistener;

                interstitial.CustomBuild();
            });
        }

        public void HideBannerAd()
        {
            if (ll != null)
            {
                fl.RemoveView(ll);
                ll.RemoveView(adView);
                adView.Dispose();
                adView = null;
                ll.Dispose();
                ll = null;
            }
        }

        public void RefreshBannerAd()
        {
            if (ll == null)
            {
                HideBannerAd();
                ll = new LinearLayout(this);
                ll.Orientation = Orientation.Horizontal;
                ll.SetGravity(GravityFlags.Top | GravityFlags.Center);

                adView = new AdView(this)
                {
                    AdUnitId  = AdMob_AdUnitID,
                    AdSize = AdSize.Banner
                };

                ll.AddView(adView);
                fl.AddView(ll);
            }

            using (var adBuilder = new AdRequest.Builder())
            {
                adBuilder.AddTestDevice(AdRequest.DeviceIdEmulator);

                foreach(string deviceID in TestDevices)
                    adBuilder.AddTestDevice(deviceID);

                var adRequest = adBuilder.Build();
                adView.LoadAd(adRequest);
            }
        }

    }
}


