using System;

public static class TextEvents
{
    public static Action<string> OnTextRequested;
    public static Action OnTextRevealCompleted;
    public static Action<char> OnCharacterRevealed;
    public static Action<string> OnLinkTriggered;
    public static Action OnPauseRequested;
    public static Action OnResumeRequested;
}