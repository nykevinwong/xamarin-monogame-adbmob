using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Ads;

namespace MonoGame3D.Droid.Tools
{

        public static class AdWrapper
        {
            public static InterstitialAd ConstructFullPageAdd(Context con, string UnitID)
            {
                var ad = new InterstitialAd(con);
                ad.AdUnitId = UnitID;
                return ad;
            }
            public static AdView ConstructStandardBanner(Context con, AdSize adsize, string UnitID)
            {
                var ad = new AdView(con);
                ad.AdSize = adsize;
                ad.AdUnitId = UnitID;
                return ad;
            }

            public static InterstitialAd CustomBuild(this InterstitialAd ad)
            {

                var requestbuilder = new AdRequest.Builder();

                ad.LoadAd(requestbuilder.Build());
                return ad;
            }
            public static AdView CustomBuild(this AdView ad)
            {
                var requestbuilder = new AdRequest.Builder();
                ad.LoadAd(requestbuilder.Build());
                return ad;
            }
        }

    public class adlistener : AdListener
    {
        // Declare the delegate (if using non-generic pattern).
        public delegate void AdLoadedEvent();
        public delegate void AdClosedEvent();
        public delegate void AdOpenedEvent();



        // Declare the event.
        public event AdLoadedEvent AdLoaded;
        public event AdClosedEvent AdClosed;
        public event AdOpenedEvent AdOpened;
        public event AdClosedEvent AdFailed;
        public override void OnAdFailedToLoad(int p0)
        {
            if (AdFailed != null) this.AdFailed();
            base.OnAdFailedToLoad(p0);
        }
        public override void OnAdLoaded()
        {
            if (AdLoaded != null) this.AdLoaded();
            base.OnAdLoaded();
        }

        public override void OnAdClosed()
        {
            if (AdClosed != null) this.AdClosed();
            base.OnAdClosed();
        }
        public override void OnAdOpened()
        {
            if (AdOpened != null) this.AdOpened();
            base.OnAdOpened();
        }
    }
}