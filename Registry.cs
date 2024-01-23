namespace RiscVCS;

public class Registry
{
    public Registry()
    {
        Register = new int[32];
    }

    public int[] Register { get; set; }

}