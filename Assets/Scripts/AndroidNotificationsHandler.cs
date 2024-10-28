#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif
using UnityEngine;
using System;

public class AndroidNotificationsHandler : MonoBehaviour
{

#if UNITY_ANDROID
    private const string CHANNEL_ID = "notification_channel";

    private void Start()
    {
        RequestNotificationPermission();
    }

    private void RequestNotificationPermission()
    {
        // Check if we are on Android 13 or above
        if (Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS") == false)
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new()
        {
            Id = CHANNEL_ID,
            Name = "Notification Channel",
            Description = "Some random desc...",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new()
        {
            Title = "Enery Recharged!",
            Text = "Your enery is fully recharged, come back to play again!",
            SmallIcon = "icon_0",
            LargeIcon = "icon_1",
            FireTime = dateTime,
            ShouldAutoCancel = true
        };

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }
#endif
}
