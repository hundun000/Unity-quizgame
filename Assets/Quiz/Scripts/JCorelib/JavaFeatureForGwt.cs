using hundun.quizlib.prototype.@event;
using System;


public static class JavaFeatureForGwt
{
    internal static T requireNonNull<T>(T value)
    {
        if (value == null)
        {
            throw new NullReferenceException();
        }
        return value;
    }
}

public class NumberFormat {
    int integerBit;
    int decimalBit;

    private NumberFormat(int integerBit, int decimalBit) {
        this.integerBit = integerBit;
        this.decimalBit = decimalBit;
    }
        
    public String format(double value) {
        String str = value.ToString();
        String[] parts = str.Split(".");
        String integerPart;
        String decimalPart;
        if (parts.Length == 1) {
            integerPart = parts[0];
            decimalPart = "";
        } else {
            integerPart = parts[0];
            decimalPart = parts[1];
        }
        while (integerPart.Length < integerBit) {
            integerPart = "0" + integerPart;
        }
        while (decimalPart.Length < decimalBit) {
            decimalPart = decimalPart + "0";
        }
        if (decimalPart.Length > decimalBit) {
            decimalPart = decimalPart.Substring(0, decimalBit);
        }
        if (!decimalPart.Equals("")) {
            decimalPart = "." + decimalPart;
        }
        return integerPart + decimalPart;
    }

    public static NumberFormat getFormat(int integerBit, int decimalBit) {
        NumberFormat result = new NumberFormat(decimalBit, decimalBit);
        return result;
    }
}