using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
namespace OyutniiBurtgel {
 public partial class Form1 : Form {
  public Form1() { InitializeComponent(); }
  private void Register(object sender, EventArgs e) {
   if (string.IsNullOrWhiteSpace(kod.Text) || !utas.MaskCompleted) { MessageBox.Show("Оюутны код, утасны дугаараа зөв оруулна уу!"); return; }
   string code = kod.Text.Trim();
   foreach (DataGridViewRow row in husnegt.Rows) if (string.Equals(Convert.ToString(row.Cells[0].Value), code, StringComparison.OrdinalIgnoreCase)) { MessageBox.Show("Энэ оюутан бүртгэгдсэн байна!"); return; }
   string details = "ОЮУТНЫ БҮРТГЭЛ\n________________________\nКод: " + code + "\nНас: " + nas.Value + "\nУтас: " + utas.Text + "\nТөрсөн өдөр: " + torson.Value.ToString("yyyy-MM-dd") + "\nСургалт: " + (surgalt.Checked ? "Өдрийн" : "Бусад") + "\nТэнхим: " + mergejil.Text + "\nТэтгэлэг: " + (tetgeleg.Checked ? "Тийм" : "Үгүй") + "\nХичээл: " + string.Join(", ", hicheel.CheckedItems.Cast<object>()) + "\nХэл: " + hel.Text;
   int i = husnegt.Rows.Add(code, utas.Text, nas.Value); husnegt.Rows[i].Tag = details;
   medeelel.Text = details; niit.Text = "Нийт: " + husnegt.Rows.Count; progress.Value = 100;
   MessageBox.Show("Амжилттай бүртгэгдлээ!");
  }
  private void ClearFields(object sender, LinkLabelLinkClickedEventArgs e) {
   kod.Clear(); utas.Clear(); tetgeleg.Checked = false; surgalt.Checked = false; nas.Value = 20; mergejil.SelectedIndex = 0; hel.SelectedIndex = 0; torson.Value = DateTime.Today.AddYears(-20);
   for(int i=0;i<hicheel.Items.Count;i++) hicheel.SetItemChecked(i,false);
   if(zurag.Image != null) { zurag.Image.Dispose(); zurag.Image = null; }
   medeelel.Clear(); progress.Value=0; kod.Focus();
  }
  private void PickImage(object sender, EventArgs e) {
   using(var dialog = new OpenFileDialog { Filter = "Зураг|*.jpg;*.jpeg;*.png;*.bmp" }) {
    if(dialog.ShowDialog()!=DialogResult.OK) return;
    try { using(var img=Image.FromFile(dialog.FileName)) { var old=zurag.Image; zurag.Image=new Bitmap(img); if(old!=null) old.Dispose(); } } catch(Exception) { MessageBox.Show("Зураг нээхэд алдаа гарлаа!"); }
   }
  }
  private void SelectDepartment(object sender, TreeViewEventArgs e) { int i=mergejil.Items.IndexOf(e.Node.Text); if(i>=0) mergejil.SelectedIndex=i; }
  private void SelectStudent(object sender, DataGridViewCellEventArgs e) { if(e.RowIndex>=0) medeelel.Text=Convert.ToString(husnegt.Rows[e.RowIndex].Tag); }
 }
}
