
/// <summary>
/// It Stores All the Event Names in the Game which makes it easier to Trigger the Event
/// </summary>
/// 
namespace FARMLIFEVR.EVENTSYSTEM
{
    public static class EventNames
    {
        // Dog related
        public const string CallPet = "call_pet";
        public const string PetSit = "sit";
        public const string PetStandUp = "stand";
        public const string PetEat = "eat";
        public const string PetDie = "die";
        public const string SwitchDogStateToIdle = "SwitchDogStateToIdle";
        public const string SwitchDogStateToEmote = "SwitchDogStateToEmote";
        
        // Land Related
        public const string MF_AdvanceToNextState = "MF_AdvanceToNextState";
        public const string MF_OnStateChanged = "MF_OnStageChanged";
        public const string StartIrrigation = "StartIrrigation";
        public const string PesticideSprayInteractableSelected = "PesticideSprayInteractableSelected";
        public const string MaxShoutCountReached = "MaxShoutCountReached";
        public const string HarvestReadyMaizeCollected = "HarvestReadyMaizeCollected";
        
        
        //Home Related
        public const string OnPlayerDedectedOutsideOfDoor = "OnPlayerDedectedOutsideOfDoor";
        public const string OnPlayerDedectedInsideOfDoor = "OnPlayerDedectedInsideOfDoor";
        
        //Mission Related
        public const string AdvanceToNextMission = "AdvanceToNextMission";
        public const string OnPlayerAcceptedMission = "OnPlayerAcceptedMission";
        public const string MissionStarted = "MissionStarted";
        public const string MissionCompleted = "MissionCompleted";
        public const string PREP_ = "PREP_";
        public const string CONC_ = "CONC_";
        
        
        // SkyBox or Time related
        public const string MakeItDay = "MakeItDay";
        public const string MakeItEvening = "MakeItEvening";
        public const string MakeItNight = "MakeItNight";
        
        
        // Player Locomotion related
        public const string EnableMovement = "EnableMovement";
        public const string DisableMovement = "DisableMovement";
        public const string AfterMissionTeleport = "AfterMissionTeleport";
        public const string SetGravity = "SetGravity";
        
        
        //Fading Screen Related
        public const string BEGIN_NightFade = "Begin_NightFade";
        public const string BEGIN_QuickFade = "Begin_QuickFade";
        public const string BEGIN_TeleFade = "Begin_TeleFade";
    }
    // Mission Preparation
    public enum EMissionType
    {
        EMT_PLOUGHING,
        EMT_PLANTING,
        EMT_GRUBBING,
        EMT_WATERING,
    }
}

