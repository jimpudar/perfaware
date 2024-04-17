// See https://aka.ms/new-console-template for more informationop

using var file = File.Open(args[0],FileMode.Open);

Console.WriteLine("bits 16\n");

var b1 = file.ReadByte();
while (b1 != -1)
{
    var b2 = file.ReadByte();

    /*
    OPCODE D W   MOD REG R/M
    100010 0 1   11  011 001
    */

    if ((b1 ^ 0b10001000) >> 2 == 0)
    {
        Console.Write("mov ");
    }
    else
    {
        throw new ApplicationException("Only mov allowed!");
    }

    var d = b1 & 2; // 1 means REG is destination; 0 means REG is source
    var w = b1 & 1;

    var mod = (b2 & 0b11000000) >>
              6; // 00 memory mode, 01 memory mode 8-bit displacement, 10 memory mode 16-bit displacement, 11 register mode
    var reg = (b2 & 0b00111000) >> 3;
    var rm = b2 & 0b00000111;

    string[][] regs =
    [
        ["al", "cl", "dl", "bl", "ah", "ch", "dh", "bh"],
        ["ax", "cx", "dx", "bx", "sp", "bp", "si", "di"]
    ];

    if (mod != 3)
    {
        throw new ApplicationException("Only register to register allowed");
    }

    if (d == 1)
    {
        Console.WriteLine($"{regs[w][reg]}, {regs[w][rm]}");
    }
    else
    {

        Console.WriteLine($"{regs[w][rm]}, {regs[w][reg]}");
    }

    b1 = file.ReadByte();
}