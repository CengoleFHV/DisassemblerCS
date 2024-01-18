namespace DisassemblerCS;

public class Encoder
{
    public Encoder()
    {

    }

    public static void encodeInstruction(uint instructionHex)
    {
        RISCVInstruction instruction = new RISCVInstruction();

        instruction.OpCode = instructionHex & 0x7f;
        instruction.Funct3 = (instructionHex & 0x7000) >> 12;

        switch (instruction.OpCode)
        {
            case 0b_110_0111:
            case 0b_000_0011:
            case 0b_001_0011:
            case 0b_111_0011:
                instruction.IEF = IEF.I;
                encodeI(instruction, instructionHex);
                break;
            case 0b_011_0111:
            case 0b_001_0111:
                instruction.IEF = IEF.U;
                instruction.Funct3 = null;
                encodeU(instruction, instructionHex);
                break;
            case 0b_110_1111:
                instruction.IEF = IEF.J;
                encodeJ(instruction, instructionHex);
                break;
            case 0b_110_0011:
                instruction.IEF = IEF.B;
                encodeB(instruction, instructionHex);
                break;
            case 0b_010_0011:
                instruction.IEF = IEF.S;
                encodeS(instruction, instructionHex);
                break;
            case 0b_011_0011:
                instruction.IEF = IEF.R;
                encodeR(instruction, instructionHex);
                break;
            default:
                throw new Exception("OpCode not Found");
        }

    }

    private static void encodeI(RISCVInstruction instruction, uint instructionHex)
    {
        instruction.Rd = (instructionHex & 0xf80) >> 7;
        instruction.Rs1 = (instructionHex & 0xf8000) >> 15;
        instruction.ImmValue = (int?)((instructionHex & 0xfff00000) >> 20);

        //check if MSB hex is over 7, two's Complement
        if (((instruction.ImmValue & 0x800) >> 11) == 1)
        {
            instruction.ImmValue = instruction.ImmValue | -0x1000;
        }

        switch (instruction.OpCode)
        {
            case 0b_110_0111:
                //jalr Instruction
                Console.WriteLine($"jalr x{instruction.Rd}, {instruction.ImmValue}(x{instruction.Rs1})");
                break;
            case 0b_000_0011:
                //lb, lh, lw, lbu, lhu Instruction
                switch (instruction.Funct3)
                {
                    case 0b_000:
                        //lb Instruction
                        Console.WriteLine($"lb x{instruction.Rd}, {instruction.ImmValue}(x{instruction.Rs1})");
                        break;
                    case 0b_001:
                        //lh Instruction
                        Console.WriteLine($"lh x{instruction.Rd}, {instruction.ImmValue}(x{instruction.Rs1})");
                        break;
                    case 0b_010:
                        //lw Instruction
                        Console.WriteLine($"lw x{instruction.Rd}, {instruction.ImmValue}(x{instruction.Rs1})");
                        break;
                    case 0b_100:
                        //lbu Instruction
                        Console.WriteLine($"lbu x{instruction.Rd}, {instruction.ImmValue}(x{instruction.Rs1})");
                        break;
                    case 0b_101:
                        //lhu Instruction
                        Console.WriteLine($"lhu x{instruction.Rd}, {instruction.ImmValue}(x{instruction.Rs1})");
                        break;
                    default:
                        throw new Exception("Funct3 Code not Found");
                }

                break;
            case 0b_001_0011:
                //addi, slti, sltiu, xori, ori, andi, slli, srli, srai Instruction
                switch (instruction.Funct3)
                {
                    case 0b_000:
                        //addi Instruction
                        Console.WriteLine($"addi x{instruction.Rd}, x{instruction.Rs1}, {instruction.ImmValue}");
                        break;
                    case 0b_001:
                        throw new NotImplementedException();
                    case 0b_010:
                        //slti Instruction
                        Console.WriteLine($"slti x{instruction.Rd}, x{instruction.Rs1}, {instruction.ImmValue}");
                        break;
                    case 0b_011:
                        //sltiu Instruction
                        Console.WriteLine($"sltiu x{instruction.Rd}, x{instruction.Rs1}, {instruction.ImmValue}");
                        break;
                    case 0b_100:
                        //xori Instruction
                        Console.WriteLine($"xori x{instruction.Rd}, x{instruction.Rs1}, {instruction.ImmValue}");
                        break;
                    case 0b_101:
                        throw new NotImplementedException();
                    case 0b_110:
                        //ori Instruction
                        Console.WriteLine($"ori x{instruction.Rd}, x{instruction.Rs1}, {instruction.ImmValue}");
                        break;
                    case 0b_111:
                        //andi Instruction
                        Console.WriteLine($"andi x{instruction.Rd}, x{instruction.Rs1}, {instruction.ImmValue}");
                        break;
                    default:
                        throw new Exception("Funct3 Code not Found");
                }
                break;
            default:
                throw new Exception("OpCode not in I Found");
        }
    }

    private static void encodeR(RISCVInstruction instruction, uint instructionHex)
    {
        instruction.Rd = (instructionHex & 0xf80) >> 7;
        instruction.Rs1 = (instructionHex & 0xf8000) >> 15;
        instruction.Rs2 = (instructionHex & 0x1f00000) >> 20;
        instruction.Funct7 = (uint?)((instructionHex & -0x2000000) >> 25);

        bool isRV32M = instruction.Funct7 == 1;
        bool isSub = instruction.Funct7 == 0b_010_000;

        if (isRV32M)
        {
            //mul, mulh, mulhsu, mulhu, div, divu, rem, remu Instructions
            switch (instruction.Funct3)
            {
                case 0b_000:
                    //mul Instruction
                    Console.WriteLine($"mul x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_001:
                    //mulh Instruction
                    Console.WriteLine($"mulh x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_010:
                    //mulhsu Instruction
                    Console.WriteLine($"mulhsu x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_011:
                    //mulhu Instruction
                    Console.WriteLine($"mulhu x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_100:
                    //div Instruction
                    Console.WriteLine($"div x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_101:
                    //divu Instruction
                    Console.WriteLine($"divu x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_110:
                    //rem Instruction
                    Console.WriteLine($"rem x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                case 0b_111:
                    //remu Instruction
                    Console.WriteLine($"remu x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                    break;
                default:
                    throw new Exception("Funct3 Code not Found");
            }
        }
        else
        {
            //add, sub, sll, slt, sltu, xor, srl, sra, or, and Instructions

            if (isSub)
            {
                switch (instruction.Funct3)
                {
                    case 0b_000:
                        //sub Instruction
                        Console.WriteLine($"sub x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_101:
                        //sra Instruction
                        Console.WriteLine($"sra x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    default:
                        throw new Exception("Funct3 Code not Found");
                }
            }
            else
            {
                switch (instruction.Funct3)
                {
                    case 0b_000:
                        //add Instruction
                        Console.WriteLine($"add x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_001:
                        //sll Instruction
                        Console.WriteLine($"sll x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_010:
                        //slt Instruction
                        Console.WriteLine($"slt x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_011:
                        //sltu Instruction
                        Console.WriteLine($"sltu x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_100:
                        //xor Instruction
                        Console.WriteLine($"xor x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_101:
                        //srl Instruction
                        Console.WriteLine($"srl x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_110:
                        //or Instruction
                        Console.WriteLine($"or x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    case 0b_111:
                        //and Instruction
                        Console.WriteLine($"and x{instruction.Rd}, x{instruction.Rs1}, x{instruction.Rs2}");
                        break;
                    default:
                        throw new Exception("Funct3 Code not Found");
                }

            }
        }
    }

    private static void encodeB(RISCVInstruction instruction, uint instructionHex)
    {
        throw new NotImplementedException("Whoopsie Poopsie 😔");
    }

    private static void encodeU(RISCVInstruction instruction, uint instructionHex)
    {
        throw new NotImplementedException("Whoopsie Poopsie 😔");
    }

    private static void encodeS(RISCVInstruction instruction, uint instructionHex)
    {
        int imm11_7 = (int)(instructionHex & 0xf80) >> 7;
        int imm31_25 = (int)((instructionHex & -0x2000000) >> 25);
        instruction.Rs1 = (instructionHex & 0xf8000) >> 15;
        instruction.Rs2 = (instructionHex & 0x1f00000) >> 20;

        instruction.ImmValue = (imm11_7 | (imm31_25 << 5));

        //check if MSB hex is over 7, two's Complement
        if (((instruction.ImmValue & 0x800) >> 11) == 1)
        {
            instruction.ImmValue = instruction.ImmValue | -0x1000;
        }

        switch (instruction.Funct3)
        {
            case 0b_000:
                //sb Instruction
                Console.WriteLine($"sb x{instruction.Rs2}, {instruction.ImmValue}(x{instruction.Rs1})");
                break;
            case 0b_001:
                //sh Instruction
                Console.WriteLine($"sh x{instruction.Rs2}, {instruction.ImmValue}(x{instruction.Rs1})");
                break;
            case 0b_010:
                //sw Instruction
                Console.WriteLine($"sw x{instruction.Rs2}, {instruction.ImmValue}(x{instruction.Rs1})");
                break;
            default:
                throw new Exception("Funct3 Code not Found");
        }

    }

    private static void encodeJ(RISCVInstruction instruction, uint instructionHex)
    {
        throw new NotImplementedException("Whoopsie Poopsie 😔");
    }
}
