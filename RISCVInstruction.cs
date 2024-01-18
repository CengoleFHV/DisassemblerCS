
namespace DisassemblerCS;

public enum IEF
{
    U,
    J,
    R,
    I,
    S,
    B
};

public class RISCVInstruction
{
    public uint OpCode { get; set; }
    public IEF IEF { get; set; }
    public uint Rd { get; set; }
    public uint? Rs1 { get; set; }
    public uint? Rs2 { get; set; }
    public uint? Funct3 { get; set; }
    public uint? Funct7 { get; set; }

    public int? ImmValue { get; set; }

}
