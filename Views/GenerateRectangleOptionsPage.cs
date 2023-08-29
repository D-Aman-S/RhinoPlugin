using System.Diagnostics;
using Eto.Drawing;
using Eto.Forms;
using Rhino;
using Rhino.Geometry;
using Rhino.UI;

namespace GenerateRectangle.Views
{
  class GenerateRectangleOptionsPage : OptionsDialogPage
  {
    private GenerateRectangleOptionsPageControl m_page_control;

    public GenerateRectangleOptionsPage()
      : base("Generate Rectangle")
    {
    }

    public override bool OnActivate(bool active)
    {
      return (m_page_control == null || m_page_control.OnActivate(active));
    }

    public override bool OnApply()
    {
      return (m_page_control == null || m_page_control.OnApply());
    }

    public override void OnCancel()
    {
      if (m_page_control != null)
        m_page_control.OnCancel();
    }

    public override object PageControl
    {
      get
      {
        return (m_page_control ?? (m_page_control = new GenerateRectangleOptionsPageControl()));
      }
    }
  }

  class GenerateRectangleOptionsPageControl : Panel
  {
        private TextBox lengthTextBox;
        private TextBox widthTextBox;
        private TextBox xTextBox;
        private TextBox yTextBox;
        private TextBox zTextBox;
        public GenerateRectangleOptionsPageControl()
    {

            lengthTextBox = new TextBox();
            widthTextBox = new TextBox();
            xTextBox = new TextBox();
            yTextBox = new TextBox();
            zTextBox = new TextBox();

            var generateButton = new Button { Text = "Generate Plate" };
            generateButton.Click += (sender, e) => GenerateRectangle();

            var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(30) };
            var headingLabel = new Label
            {
                Text = "Generate Rectangular Flat Plate (For Flat Layups)",
                Font = new Font(SystemFont.Default.ToString(), 8,FontStyle.Bold) 
            };
            layout.BeginVertical();
            layout.BeginHorizontal();
            layout.Add(null);
            layout.Add(headingLabel);
            layout.Add(null);
            layout.EndHorizontal();
            layout.AddRow(null);
            layout.AddRow(null);
            layout.AddRow(new Label { Text = "Length(Lx):" }, lengthTextBox);
            layout.AddRow(new Label { Text = "Width(Ly):" }, widthTextBox);
            layout.AddRow(new Label { Text = "X:" }, xTextBox);
            layout.AddRow(new Label { Text = "Y:" }, yTextBox);
            layout.AddRow(new Label { Text = "Z:" }, zTextBox);
            layout.AddRow(null);
            layout.AddAutoSized(generateButton);
            layout.EndVertical();
            
            Content = layout;
        }

    public bool OnActivate(bool active)
    {
      Debug.WriteLine("GenerateRectangleOptionsDialogPage.OnActive(" + active + ")");
      return true;
    }

    public bool OnApply()
    {
      Debug.WriteLine("GenerateRectangleOptionsDialogPage.OnApply()");
      return true;
    }

    public void OnCancel()
    {
      Debug.WriteLine("GenerateRectangleOptionsDialogPage.OnCancel()");
    }


        private void GenerateRectangle()
        {
            if (double.TryParse(widthTextBox.Text, out double width) &&
                double.TryParse(lengthTextBox.Text, out double height) &&
                double.TryParse(xTextBox.Text, out double centerX) &&
                double.TryParse(yTextBox.Text, out double centerY) &&
                double.TryParse(zTextBox.Text, out double centerZ))
            {
                Point3d center = new Point3d(centerX, centerY, centerZ);             
                Plane plane = new Plane(center, Vector3d.ZAxis);
                Rectangle3d rectangle = new Rectangle3d(plane, width, height);
                RhinoDoc.ActiveDoc.Objects.AddRectangle(rectangle);
                RhinoDoc.ActiveDoc.Views.Redraw();
                RhinoApp.WriteLine("Rectangle added to the document.");
            }
            else
            {
                RhinoApp.WriteLine("Invalid input values.");
            }
        }
  }
}
