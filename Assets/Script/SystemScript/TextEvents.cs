using System;

public static class TextEvents
{
    public static Action<string> OnTextRequested;
    public static Action OnTextRevealCompleted;
    public static Action<char> OnCharacterRevealed;
}