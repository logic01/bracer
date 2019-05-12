using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Linq;

namespace PR.Export.Utility
{
    public static class Formatting
    {

        public static void CreateAndAddCharacterStyle(WordprocessingDocument doc, string styleId, string styleName, string aliases, string linkedStyle, IEnumerable<OpenXmlElement> wordStyles)
        {
            // Get the Styles part for this document.
            StyleDefinitionsPart part =
                doc.MainDocumentPart.StyleDefinitionsPart;

            // If the Styles part does not exist, add it.
            if (part == null)
            {
                part = AddStylesPartToPackage(doc);
            }

            // Get access to the root element of the styles part.
            Styles styles = part.Styles;

            // Create a new character style and specify some of the attributes.
            Style style = new Style()
            {
                Type = StyleValues.Character,
                StyleId = styleId,
                CustomStyle = true
            };

            // Create and add the child elements (properties of the style).
            Aliases aliases1 = new Aliases() { Val = aliases };
            StyleName styleName1 = new StyleName() { Val = styleName };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = linkedStyle };
            if (aliases != "")
                style.Append(aliases1);
            style.Append(styleName1);
            style.Append(linkedStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
       
            foreach(var wordStyle in wordStyles)
            {
                styleRunProperties1.Append(wordStyle);
            }
            
            // Add the run properties to the style.
            style.Append(styleRunProperties1);

            // Add the style to the styles part.
            styles.Append(style);
        }

        public static void AddNewStyle(WordprocessingDocument doc, string styleId, string styleName, Paragraph p)
        {
            // If the paragraph has no ParagraphProperties object, create one.
            if (p.Elements<ParagraphProperties>().Count() == 0)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get the paragraph properties element of the paragraph.
            ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();

            StyleDefinitionsPart titleStyle = doc.MainDocumentPart.StyleDefinitionsPart;
            if (titleStyle == null)
            {
                titleStyle = AddStylesPartToPackage(doc);
                AddNewStyle(titleStyle, styleId, styleName);
            }
            else
            {
                // If the style is not in the document, add it.
                if (IsStyleIdInDocument(doc, styleId) != true)
                {
                    // No match on styleid, so let's try style name.
                    string styleidFromName = GetStyleIdFromStyleName(doc, styleName);
                    if (styleidFromName == null)
                    {
                        AddNewStyle(titleStyle, styleId, styleName);
                    }
                    else
                        styleId = styleidFromName;
                }
            }

            // Set the style of the paragraph.
            pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleId };
        }

        // Return styleid that matches the styleName, or null when there's no match.
        private static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                .Where(s => s.Val.Value.Equals(styleName) &&
                    (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }

        // Return true if the style id is in the document, false otherwise.
        private static bool IsStyleIdInDocument(WordprocessingDocument doc,
            string styleid)
        {
            // Get access to the Styles element for this document.
            Styles s = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

            // Check that there are styles and how many.
            int n = s.Elements<Style>().Count();
            if (n == 0)
                return false;

            // Look for a match on styleid.
            Style style = s.Elements<Style>()
                .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style == null)
                return false;

            return true;
        }

        // Create a new style with the specified styleid and stylename and add it to the specified
        // style definitions part.
        private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart,
            string styleid, string stylename)
        {
            // Get access to the root element of the styles part.
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            StyleName styleName1 = new StyleName() { Val = stylename };

            //The styles set below probably should be passed in as a parameter and not hard coded, but
            //I only care about the Titles that are being append, so just leaving it for now.
            BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
          
            // Specify a 12 point size.
            FontSize fontSize1 = new FontSize() { Val = "24" };            
            styleRunProperties1.Append(fontSize1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);

            // Add the style to the styles part.
            styles.Append(style);
        }


        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        private static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            StyleDefinitionsPart part;
            part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();
            root.Save(part);
            return part;
        }

    }
}
