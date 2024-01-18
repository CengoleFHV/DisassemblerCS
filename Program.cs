// See https://aka.ms/new-console-template for more information

using DisassemblerCS;

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

Encoder RISCVEncoder = new Encoder();

foreach (var instruction in squareArray)
{
    Encoder.encodeInstruction(instruction);
}
