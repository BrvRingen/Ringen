using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;

namespace Ringen.Plugin.CsEditor.Helper
{
    public static class PdfHelper
    {

        public static Paragraph AddAbstandNachOben(string abstand)
        {
            Paragraph p = new Paragraph();
            p.Format.LineSpacingRule = LineSpacingRule.Exactly;
            p.Format.LineSpacing = "0mm";
            p.Format.SpaceBefore = abstand;

            return p;
        }
    }
}
