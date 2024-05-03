namespace War3Api.Generator.Object;

public class Utils
{
    public static string IdToFourCc(int value)
    {
        const string charMap =
            ".................................!.#$%&'()*+,-./0123456789:;<=>.@ABCDEFGHIJKLMNOPQRSTUVWXYZ[.]^_`abcdefghijklmnopqrstuvwxyz{|}~.................................................................................................................................";
        var result = "";
        var remainingValue = value;

        for (var i = 0; i < 4; i++)
        {
            var charValue = remainingValue % 256;

            remainingValue /= 256;
            result += charMap[charValue];
        }

        return result;
    }
}