//neue Main Function in .Net 8

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

try {
    //neue RiscV Instanze wird erschaffen
    RiscV risky = new RiscV();

    //Instructions werden in den InstructionRegistry geladen
    risky.AddInstructions(squareArray);

    //InstructionRegistry abarbeiten
    risky.RunInstructions();

    //dump it https://i.redd.it/bn368ds487m71.jpg
    risky.dumpInstructionRegistry();
    risky.dumpRegistry();
    risky.dumpMemory();
} catch (Exception e) {
    Console.WriteLine($"Exception: {e.Message}");
    throw;
}