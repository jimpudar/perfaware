namespace part1_ex2;

class Program
{
    static string[][] regs =
    [
        ["al", "cl", "dl", "bl", "ah", "ch", "dh", "bh"],
        ["ax", "cx", "dx", "bx", "sp", "bp", "si", "di"]
    ];
    
    static void Main(string[] args)
    {
        using var file = File.Open(args[0], FileMode.Open);

        Console.WriteLine("bits 16\n");

        var b1 = file.ReadByte();
        while (b1 != -1)
        {

            switch (b1 >> 2)
            {
                case 0b100010:
                    mov_reg_mem_to_from_reg(b1, file);
                    break;
                case 0b110001:
                    mov_immediate_to_reg_mem(b1, file);
                    break;
                default:
                    throw new NotImplementedException("operation {opcode:b8} not implemented");
            }
            
            b1 = file.ReadByte();
        }
    }

    public static void mov_reg_mem_to_from_reg(int b1, FileStream file)
    {
        /*
            OPCODE D W   MOD REG R/M
            100010 0 1   11  011 001
        */

        var b2 = file.ReadByte();

        switch (b2 >> 6)
        {
            case 0b00:
                if ((b2 & 0b00000111) == 0b110)
                {
                    direct_address(b1, b2, file);
                }
                else
                {
                    memory_mode_no_displacement(b1, b2, file);
                }
                break;
            case 0b01:
                memory_mode_8_bit_displacement(b1, b2, file);
                break;
            case 0b10:
                memory_mode_16_bit_displacement(b1, b2, file);
                break;
            case 0b11:
                register_mode(b1, b2, file);
                break;
        }

    }

    public static void direct_address(int b1, int b2, FileStream file)
    {
        
    }
    public static void memory_mode_no_displacement(int b1, int b2, FileStream file)
    {
        
    }
    public static void memory_mode_8_bit_displacement(int b1, int b2, FileStream file)
    {
        
    }
    public static void memory_mode_16_bit_displacement(int b1, int b2, FileStream file)
    {
        
    }
    public static void register_mode(int b1, int b2, FileStream file)
    {
        var d = b1 & 2; // 1 means REG is destination; 0 means REG is source
        var w = b1 & 1;

        var reg = (b2 & 0b00111000) >> 3;
        var rm = b2 & 0b00000111;

        Console.WriteLine(d == 1 ? $"{regs[w][reg]}, {regs[w][rm]}" : $"{regs[w][rm]}, {regs[w][reg]}");
    }

    public static void mov_immediate_to_reg_mem(int b1, FileStream file)
    {
        
    }
}