using UnityEngine;
using Vuforia;

public class ReadTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public TrackableBehaviour mTrackableBehaviour;
    public ReadObjectBehaviour contentObject;
    public TrackableKeyContainer containedKey;

    void OnEnable()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    void OnDisable()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, 
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            OnTrackingLost();
        }
    }

    void OnTrackingFound()
    {
        ReadGameplayTrackingManager.instance.AddTrackedObject(contentObject);
    }

    void OnTrackingLost()
    {
        ReadGameplayTrackingManager.instance.RemoveTrackedObject(contentObject);
    }
}