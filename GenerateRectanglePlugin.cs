using Rhino;
using System;
using System.Collections.Generic;
using Rhino.UI;
namespace GenerateRectangle
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class GenerateRectanglePlugin : Rhino.PlugIns.PlugIn
    {
        public GenerateRectanglePlugin()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the GenerateRectanglePlugin plug-in.</summary>
        public static GenerateRectanglePlugin Instance { get; private set; }

        protected override void DocumentPropertiesDialogPages(RhinoDoc doc, List<OptionsDialogPage> pages)
        {
            var page = new Views.GenerateRectangleOptionsPage();
            pages.Add(page);
        }

        protected override void OptionsDialogPages(List<OptionsDialogPage> pages)
        {
            var page = new Views.GenerateRectangleOptionsPage();
            pages.Add(page);
        }
    }
}