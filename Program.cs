// See https://aka.ms/new-console-template for more information

using RiscVCS;

uint[] squareArray = [
    0xFE010113,
    0x00812E23,
    0x02010413,
    0xFEA42623,
    0xFEC42783,
    0x02F787B3,
    0x00078513,
    0x01C12403,
    0x02010113,
    0x00008067,
];

//uint[] allInstructionsTestArray = [
//    0x000450b7, //lui x1, 69
//    0x00045097, //auipc x1, 69
//    0x0100006f, //jal x0, 16
//    0xf0345067, //jalr x0, -253(x8)

//    0x00308163, //beq x1, x3, 2
//    0x00309163, //bne x1, x3, 2
//    0x0030c163, //blt x1, x3, 2
//    0x0030d163, //bge x1, x3, 2
//    0x0030e163, //bltu x1, x2, 2
//    0x0030f163, //bgeu x1, x2, 2

//    0x00000083, //lb x1, 0(x0)
//    0x00001083, //lh x1, 0(x0)
//    0x00002083, //lw x1, 0(x0)
//    0x00004083, //lbu x1, 0(x0)
//    0x00005083, //lhu x1, 0(x0)

//    0x00810E23, //sb x8, 28(x2)
//    0x00811E23, //sh x8, 28(x2)
//    0x00812E23, //sw x8, 28(x2)

//    0xFE010113, //addi x2, x2, -32
//    0xFE012113, //slti x2, x2, -32
//    0xFE013113, //sltiu x2, x2, -32
//    0xFE014113, //xori x2, x2, -32
//    0xFE016113, //ori x2, x2, -32
//    0xFE017113, //andi x2, x2, -32
//];

try {
    RiscV risky = new RiscV();

    risky.AddInstructions(squareArray);

    risky.RunInstructions();

    risky.dumpInstructionRegistry();
    risky.dumpRegistry();
    risky.dumpMemory();
}
catch (Exception e) {
    Console.WriteLine($"Exception: {e.Message}");
    throw;
}