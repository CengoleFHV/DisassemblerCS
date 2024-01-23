namespace RiscVCS;

public class RiscV
{
    public RiscV()
    {
        PC = 1;
        Registry = new int[32];
        Registry[0] = 0x0;
        InstructionRegistry = new List<uint>(0);
        Encoder = new Encoder();
        Memory = new Dictionary<int, int>();
    }

    public int[] Registry { get; set; }
    public List<uint> InstructionRegistry { get; set; }
    public int PC { get; set; }
    public Encoder Encoder { get; set; }
    public Dictionary<int, int> Memory { get; set; }

    public void AddInstructions(uint[] instructions)
    {
        InstructionRegistry.AddRange(instructions);
    }

    public void dumpInstructionRegistry()
    {
        Console.WriteLine(String.Format("{0,-76}", String.Format("{0," + ((76 + "Instruction Registry Dump".Length) / 2).ToString() + "}", "Instruction Registry Dump")));
        Console.WriteLine("".PadRight(76, '-'));
        Console.WriteLine(String.Format("|{0,5}  |{1,12}  |{2,12}  |{3,34}  |", "", "Decimal", "Hex", "Binary"));
        Console.WriteLine("".PadRight(76, '-'));

        for (int i = 0; i < InstructionRegistry.Count; i++)
        {
            Console.WriteLine(String.Format("|{0,5}  |{1,12}  |{2,12}  |{3,34}  |", $"i{i}", $"{InstructionRegistry[i]}", $"0x{InstructionRegistry[i].ToString("X8")}", $"{InstructionRegistry[i].ToString("B32")}"));

        }

        Console.WriteLine("".PadRight(76, '-'));
        Console.WriteLine();
    }

    public void dumpRegistry()
    {
        Console.WriteLine(String.Format("{0,-76}", String.Format("{0," + ((77 + "Registry Dump".Length) / 2).ToString() + "}", "Registry Dump")));
        Console.WriteLine("".PadRight(76, '-'));
        Console.WriteLine(String.Format("|{0,5}  |{1,12}  |{2,12}  |{3,34}  |", "", "Decimal", "Hex", "Binary"));
        Console.WriteLine("".PadRight(76, '-'));

        for (int i = 0; i < Registry.Length; i++)
        {
            Console.WriteLine(String.Format("|{0,5}  |{1,12}  |{2,12}  |{3,34}  |", $"x{i}", $"{Registry[i]}", $"0x{Registry[i].ToString("X8")}", $"{Registry[i].ToString("B32")}"));
        }
        Console.WriteLine("".PadRight(76, '-'));
        Console.WriteLine();

    }

    public void dumpMemory()
    {
        Console.WriteLine(String.Format("{0,-87}", String.Format("{0," + ((87 + "Memory Dump".Length) / 2).ToString() + "}", "Memory Dump")));
        Console.WriteLine("".PadRight(87, '-'));
        Console.WriteLine(String.Format("|{0,16}  |{1,12}  |{2,12}  |{3,34}  |", "Memory Address", "Decimal", "Hex", "Binary"));
        Console.WriteLine("".PadRight(87, '-'));


        var sorted = Memory.OrderByDescending(a => a.Key);

        foreach (var block in sorted)
        {
            var memoryValue = block.Value;
            var memoryAddress = block.Key;
            Console.WriteLine(String.Format("|{0,16}  |{1,12}  |{2,12}  |{3,34}  |", $"0x{memoryAddress.ToString("X8")}", $"{block.Value}", $"0x{memoryValue.ToString("X8")}", $"{memoryValue.ToString("B32")}"));
        }
        Console.WriteLine("".PadRight(87, '-'));
        Console.WriteLine();
    }

    public void writeRegisterValue(uint index, int value)
    {
        if (index >= 1 && index < 32)
        {
            Registry[index] = value;
        }
        else if (index == 0)
        {
            return;
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

    public void writeMemoryValue(uint memoryAddress, int value)
    {
        Memory[(int)memoryAddress] = value;
    }

    public int readMemoryValue(uint memoryAddress)
    {
        var memoryValue = Memory.FirstOrDefault(x => x.Key == memoryAddress).Value;

        return memoryValue;
    }

    public void RunInstructions()
    {
        do
        {
            Encoder.encodeInstruction(InstructionRegistry[PC - 1], this);
        } while (PC != 0);
    }
}
