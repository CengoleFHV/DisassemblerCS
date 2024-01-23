namespace RiscVCS;

public class RiscV
{
    public RiscV()
    {
        PC = 0;
        Registry = new int[32];
        Registry[0] = 0x0;
        InstructionRegistry = new List<uint>(0);
        Encoder = new Encoder();
    }

    public int[] Registry { get; set; }
    public List<uint> InstructionRegistry { get; set; }
    public int PC { get; set; }
    public Encoder Encoder { get; set; }

    public void AddInstructions(uint[] instructions)
    {
        InstructionRegistry.AddRange(instructions);
    }

    public void dumpInstructionRegistry()
    {
        Console.WriteLine(String.Format("{0,-75}", String.Format("{0," + ((75 + "Instruction Registry Dump".Length) / 2).ToString() + "}", "Instruction Registry Dump")));
        Console.WriteLine("".PadRight(75, '-'));
        Console.WriteLine(String.Format("|{0,6}  |{1,12}  |{2,12}  |{3,34}  |", "", "Decimal", "Hex", "Binary"));
        Console.WriteLine("".PadRight(75, '-'));

        for (int i = 0; i < InstructionRegistry.Count; i++)
        {
            Console.WriteLine(String.Format("|{0,6}  |{1,12}  |{2,12}  |{3,34}  |", $"i{i}", $"{InstructionRegistry[i]}", $"0x{InstructionRegistry[i].ToString("X8")}", $"{InstructionRegistry[i].ToString("B32")}"));

        }

        Console.WriteLine("".PadRight(75, '-'));
    }

    public void dumpRegistry()
    {
        Console.WriteLine(String.Format("{0,-75}", String.Format("{0," + ((75 + "Registry Dump".Length) / 2).ToString() + "}", "Registry Dump")));
        Console.WriteLine("".PadRight(75, '-'));
        Console.WriteLine(String.Format("|{0,6}  |{1,12}  |{2,12}  |{3,34}  |", "", "Decimal", "Hex", "Binary"));
        Console.WriteLine("".PadRight(75, '-'));

        for (int i = 0; i < Registry.Length; i++)
        {
            Console.WriteLine(String.Format("|{0,6}  |{1,12}  |{2,12}  |{3,34}  |", $"x{i}", $"{Registry[i]}", $"0x{Registry[i].ToString("X8")}", $"{Registry[i].ToString("B32")}"));
        }
        Console.WriteLine("".PadRight(75, '-'));

    }

    public void writeRegisterValue(uint index, int value)
    {
        if (index >= 1 && index < 32)
        {
            Registry[index] = value;
        }
        else
        {
            throw new ArgumentOutOfRangeException($"Register Index is out of Range {index}");
        }
    }

    public int readRegisterValue(uint index)
    {
        if (index >= 0 && index < 32)
        {
            return Registry[index];
        }
        else
        {
            throw new ArgumentOutOfRangeException($"Register Index is out of Range {index}");
        }
    }

    public void RunInstructions()
    {
        for (int i = 0; PC < InstructionRegistry.Count; PC++)
        {
            Encoder.encodeInstruction(InstructionRegistry[PC], this);
        }
    }
}
