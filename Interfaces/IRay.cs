/*
 * This interface is used for both types of rays (primary and teleport)
 * which are used in the game.
 */

public interface IRay 
{
    public void ShowUIPrompt(bool isVisible, string text = "");
}
