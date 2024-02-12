namespace RiscVCS;

/*
 * Instruction Klasse die die Gemeinsamkeiten die alle Instructions mit sich teilen in einer Klasse
 * verbinden soll.
 */

public enum IEF {
    U,
    J,
    R,
    I,
    S,
    B
};

public class Instruction {
    public uint OpCode { get; set; }
    public IEF IEF { get; set; }
    public uint Rd { get; set; }
    public uint Rs1 { get; set; }
    public uint Rs2 { get; set; }
    public uint? Funct3 { get; set; }
    public uint Funct7 { get; set; }

    private int _immValue;

    public int ImmValue {
        get => _immValue;
        set {
            //check if MSB hex is over 7, two's Complement
            if ((value & 0x800) >> 11 == 1) {
                _immValue = value | -0x1000;
            } else {
                _immValue = value;
            }
        }
        /*TODO: Two's Complement am besten mit bitshifting lösen statt dem zerhackten müll
         * >> zieht den MSB durch alles durch (0b_1000 >> 2 würde zu 0b_1110 werden)
         * in C# gibt es den >>> Operator der den MSB nicht mitziehtsch
         */
    }

}
