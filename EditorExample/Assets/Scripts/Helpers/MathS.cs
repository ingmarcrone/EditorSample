using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathS
{
    /// <summary>
    /// || "ProcentueleLocatie" van derde getal, tussen twee andere getallen". Het derde getal moet tussen de twee andere getallen vallen || 
    /// </summary>
    /// <param name="kleinsteGetal"></param>
    /// <param name="grootsteGetal"></param>
    /// <param name="positieGetal_3eGetal"></param>
    /// <returns></returns>
    public static float ProcentueleLocatie_VoorFloats(float kleinsteGetal, float grootsteGetal, float positieGetal_3eGetal)
    {
        // controlleer of de methode kan werken
        if (kleinsteGetal >= grootsteGetal)
            return -1;
        if (positieGetal_3eGetal < kleinsteGetal || positieGetal_3eGetal > grootsteGetal)
            return -1;

        // Stel alle getallen in, naar het nulpount van het kleinste getal
        float nieuwGrootsteGetal = kleinsteGetal >= 0 ? grootsteGetal - kleinsteGetal : grootsteGetal + System.Math.Abs(kleinsteGetal);
        float nieuwPositieGetal = kleinsteGetal >= 0 ? positieGetal_3eGetal - kleinsteGetal : positieGetal_3eGetal + System.Math.Abs(kleinsteGetal);

        // Reken de procentuele positie uit.
        return (100 / nieuwGrootsteGetal) * nieuwPositieGetal;
    }

    /// <summary>
    /// || "Getal_OpBasisVan_ProcentueleLocatie" derde getal tussen twee getallen op basis van "procentueleLocatie" gezien vanuit eerste getal ||
    /// </summary>
    /// <param name="eersteGetal"></param>
    /// <param name="tweedeGetal"></param>
    /// <param name="procentueleLocatie"></param>
    /// <returns></returns>
    public static float Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(float eersteGetal, float tweedeGetal, float procentueleLocatie)
    {
        if (eersteGetal == tweedeGetal)
            return eersteGetal;

        float verschilTussenEersteEnTeedeGetal = 0;

        if (eersteGetal < tweedeGetal)
        {
            // 1e getal ...>... 2e getal
            // 1.0 ........>... 3.00
            verschilTussenEersteEnTeedeGetal = eersteGetal < 0 ? tweedeGetal + System.Math.Abs(eersteGetal) : tweedeGetal - System.Math.Abs(eersteGetal);
            return eersteGetal + ((verschilTussenEersteEnTeedeGetal / 100) * procentueleLocatie);
        }
        else if (eersteGetal > tweedeGetal)
        {
            // 1e getal ...>... 2e getal
            // -1.0 .......>... -3.00
            if (tweedeGetal < 0)
                verschilTussenEersteEnTeedeGetal = eersteGetal + System.Math.Abs(tweedeGetal);
            else if (tweedeGetal > 0)
                verschilTussenEersteEnTeedeGetal = eersteGetal - System.Math.Abs(tweedeGetal);

            return eersteGetal - ((verschilTussenEersteEnTeedeGetal / 100) * procentueleLocatie);
        }

        return -99999;
    }

}
