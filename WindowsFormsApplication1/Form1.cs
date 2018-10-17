using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		public static int squereLenght = 200;
		private Form2 form2 = null;
		private Form3 form3 = null;

		int[,] SourceArray;
		int[,] ResultArray;
		int numberFigure;

		public Form1()
		{
			InitializeComponent();
		}

		// ==== Обработчики нажания на ел. панели =====
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			resetImages();
			selectBMP();
		}
		private void contureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Labeling();
		}
		// ============================================

		private void selectBMP()
		{
			openFileDialog1.Filter = "Только BMP|*.bmp|А лучше только JPG|*.jpg;*.jpeg|Хотя нет, лучше все|*.*";
			// Открыли и перевели bmp в массив 0/1
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
				Bitmap bitmap = pictureBox1.Image as Bitmap;

				SourceArray = new int[squereLenght, squereLenght];
				ResultArray = new int[squereLenght, squereLenght];
				numberFigure = 0;
				SourceArray = bitmapToArr(bitmap);
				Labeling();
			}
		}

		//====== Рекурсивный метод ======
		private void Labeling()
		{
			if (pictureBox1.Image == null)
			{
				showWarning();
			}
			else
			{
				int L = 1;
				for (int y = 0; y < squereLenght; ++y)
				{
					for (int x = 0; x < squereLenght; ++x)
					{
						Fill(SourceArray, ResultArray, x, y, L++);
					}
				}
				ResultArray = recalculateArray(ResultArray);
				buildResultImage();
				//buildBitmapImage();
			}
		}
		private void Fill(int[,] img, int[,] labels, int x, int y, int L)
		{
			// Если лейбл пустрой и на исходнике есть фигура
			if ((labels[x, y] == 0) && (img[x, y] == 1))
			{
				labels[x, y] = L;
				// Лейбл перезаписывается и заполняет соседние пиксели
				// Console.WriteLine(Convert.ToString(x) +":"+ Convert.ToString(y) +" L-"+ Convert.ToString(L));
				if (x > 0) { Fill(img, labels, x - 1, y, L); }
				if (x < squereLenght - 1) { Fill(img, labels, x + 1, y, L); }
				if (y > 0) { Fill(img, labels, x, y - 1, L); }
				if (y < squereLenght - 1) { Fill(img, labels, x, y + 1, L); }
			}
		}
		//====== ======= ====== 



		// Изменение номеров на порядковые 
		private int[,] recalculateArray(int[,] initialArr)
		{
			numberFigure = 1;
			Dictionary<int, int> actualNumbers = new Dictionary<int, int>();
			for (int y = 0; y < squereLenght; y++)
			{
				for (int x = 0; x < squereLenght; x++)
				{
					int pixelV = initialArr[y, x];
					if (pixelV != 0)
					{
						if (!actualNumbers.ContainsKey(pixelV))
						{
							actualNumbers.Add(pixelV, numberFigure++);
						}
						initialArr[y, x] = actualNumbers[pixelV];
					}
				}
			}

			return initialArr;
		}

		// Прорисовка по ResultArray
		private void buildBitmapImage()
		{
			// Начинаем рисовать
			Bitmap bitmap1 = new Bitmap(squereLenght, squereLenght);
			Dictionary<int, Color> palette = new Dictionary<int, Color>();
			for (int i = 0; i < squereLenght; i++)
			{
				for (int j = 0; j < squereLenght; j++)
				{
					int pixelV = ResultArray[i, j];
					if (pixelV != 0)
					{
						if (!palette.ContainsKey(pixelV))
						{
							int h = 255 / numberFigure;
							Console.WriteLine(getS(pixelV, ResultArray));
							//Color randomColor = getRandomColorNew(pixelV, h);
							Color randomColor = getRandomColor();
							palette.Add(pixelV, randomColor);
						}
						bitmap1.SetPixel(i, j, palette[pixelV]);
					}
					else { bitmap1.SetPixel(i, j, Color.White); }
				}
			}
			// pictureBox2.Image = bitmap1;
		}
		// Прорисовка по ResultArray
		private void buildResultImage()
		{
			int s = 675;
			// Начинаем рисовать
			Bitmap bitmap1 = new Bitmap(squereLenght, squereLenght);
			Dictionary<int, int> checkList = new Dictionary<int, int>();
			for (int i = 0; i < squereLenght; i++)
			{
				for (int j = 0; j < squereLenght; j++)
				{
					int pixelV = ResultArray[i, j];
					if (!checkList.ContainsKey(pixelV))
					{
						int sq = getS(pixelV, ResultArray);
						checkList.Add(pixelV, getS(pixelV, ResultArray));
						Console.WriteLine(sq);
					}
					if (checkList[pixelV] == s) { bitmap1.SetPixel(i, j, Color.Black); }
					else { bitmap1.SetPixel(i, j, Color.White); }
				}
			}
			//pictureBox2.Image = bitmap1;
		}

		// Обработчики построения Bitmap-a
		private void button1_Click(object sender, EventArgs e)
		{
			buildBitMapForm(ResultArray, getForm2(), getForm2().getRichTextBox());

		}
		private void button2_Click(object sender, EventArgs e)
		{
			buildBitMapForm(ResultArray, getForm3(), getForm3().getRichTextBox());
			GetNumberOfOBJ(ResultArray);
			CountOfHoles(SourceArray);

		}

		// Метод построение Bitmap-a
		public void buildBitMapForm(int[,] array, Form form, RichTextBox textBox)
		{
			// построить бит карту второго
			if ((pictureBox1.Image == null) && form != null)
			{
				showWarning();
			}
			else
			{
				form.Visible = true;
				string s = string.Empty;
				for (int i = 0; i < squereLenght; i++)
				{
					for (int j = 0; j < squereLenght; j++)
					{
						int pixelV = array[j, i];
						// bool active = Convert.ToBoolean(pixelV);
						// string symbol = active ? "1" : "0";
						string symbol = pixelV == 0 ? "_" : Convert.ToString(pixelV);
						s += symbol + " ";
					}
					s = s + "\n";
				}
				textBox.Text = s;
			}
		}


		// Вспомогательный хлам
		public int getS(int figure, int[,] img)
		{
			int result = 0;
			for (int y = 0; y < squereLenght; ++y)
			{
				for (int x = 0; x < squereLenght; ++x)
				{
					if (img[x, y] == figure) { result++; }
				}
			}
			return result;
		}
		public int[,] bitmapToArr(Bitmap bitmap)
		{
			int[,] result = new int[squereLenght, squereLenght];
			for (int i = 0; i < squereLenght; i++)
			{
				for (int j = 0; j < squereLenght; j++)
				{
					Color c = bitmap.GetPixel(i, j);
					string sc = Convert.ToString(c);
					if (sc == "Color [A=255, R=0, G=0, B=0]")
					{
						result[i, j] = 0; // Черный это 1
					}
					else result[i, j] = 1; // Белый это 0
				}
			}


			return result;
		}

		public int GetNumberOfOBJ(int[,] image)
		{
			var max = (from int x in image select x).Max();



			textBox1.Text = max.ToString();
			Console.WriteLine("Max = " + max);
			return max;
		}
		public void CountOfHoles(int[,] image)
		{
			int points1 = 0;

			int points3 = 0;




			int rows = image.GetUpperBound(0) + 1;
			int columns = image.Length / rows;
			for (int i = 1; i < columns - 1; i++)
			{
				for (int j = 1; j < rows - 1; j++)
				{
					if (image[i, j] == 0)
					{
						if ((image[i, j - 1] != 0) && (image[i - 1, j] != 0))
						{
							points1++;
						}
						if ((image[i - 1, j] != 0) && (image[i, j + 1] != 0))
						{
							points1++;
						}
						if ((image[i, j + 1] != 0) && (image[i + 1, j] != 0))
						{
							points1++;
						}
						if ((image[i + 1, j] != 0) && (image[i, j - 1] != 0))
						{
							points1++;
						}
					}
					else
					{
						if ((image[i, j - 1] != 1) && (image[i - 1, j] !=
				1))
						{
							points3++;
						}
						if ((image[i - 1, j] != 1) && (image[i, j + 1] !=
			  1))
						{
							points3++;
						}
						if ((image[i, j + 1] != 1) && (image[i + 1, j] !=
			   1))
						{
							points3++;
						}
						if ((image[i + 1, j] != 1) && (image[i, j - 1] !=
			   1))
						{
							points3++;
						}
					}
				}
			}


			Console.WriteLine(points1);
			Console.WriteLine(points3);

			int NumOFHoles = ((points3 - points1) / 4);
			int Eu = Int32.Parse(textBox1.Text) - NumOFHoles;
			textBox3.Text = Eu.ToString();
			Console.WriteLine(Eu);
			//	int Eu = buildResultImage();
			//	Console.WriteLine(Eu);

		}


		public void showWarning()
		{
			DialogResult dr = MessageBox.Show("Сначала необходимо открыть изображение!", "", MessageBoxButtons.OK);
		}
		public Color getRandomColor()
		{
			Random rnd = new Random((int)DateTime.Now.Ticks);
			return Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
		}
		public Color getRandomColorNew(int n, int h)
		{
			int r = n * h;
			return Color.FromArgb(r, 255 - r, 125);
		}
		public void resetImages()
		{
			pictureBox1.Image = null;
			//pictureBox2.Image = null;
		}
		// Геттеры и сеттеры
		public Form2 getForm2()
		{
			if (form2 == null) { form2 = new Form2(); }
			return form2;
		}
		public Form3 getForm3()
		{
			if (form3 == null) { form3 = new Form3(); }
			return form3;
		}

	}
}
