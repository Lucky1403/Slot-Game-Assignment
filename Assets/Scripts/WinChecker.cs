// This will handle all win condition checking and payout calculation. Win ONLY when all 3 middle symbols are identical.
public class WinChecker
{
    // Payout for each symbol when all 3 match
    // Index: 0=Seven, 1=Cherry, 2=Bell, 3=BAR
    private static readonly int[] payouts = { 500, 200, 150, 100 };

    // Returns payout if all 3 results match, 0 otherwise.
    // No partial wins — must be exact 3-of-a-kind.
    public static int CheckWin(int reel1, int reel2, int reel3)
    {
        if (reel1 == reel2 && reel2 == reel3)
            return payouts[reel1];

        return 0; // no win
    }

    // Returns symbol name by index for display in popup.
    public static string GetSymbolName(int index)
    {
        switch (index)
        {
            case 0: return "SEVEN";
            case 1: return "CHERRY";
            case 2: return "BELL";
            case 3: return "BAR";
            default: return "UNKNOWN";
        }
    }
}