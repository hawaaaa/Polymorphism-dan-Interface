using System;
using System.Collections.Generic;
public interface HarusBayar
{
	decimal DapatJumlahPembayaran(); // menghitung pembayaran; tidak ada implementasi
}
public class Tagihan : HarusBayar
{
	public string PartNumber { get; }
	public string PartDescription { get; }
	private int quantity;
	private decimal hargaPerBarang;

	// konstruktor empat parameter
	public Tagihan(string partNumber, string partDescription, int quantity,
	  decimal hargaPerBarang)
	{
		PartNumber = partNumber;
		PartDescription = partDescription;
		Quantity = quantity; // memvalidasi quantity
		HargaPerBarang = hargaPerBarang; // memvalidasi harga per barang
	}
	public int Quantity
	{
		get
		{
			return quantity;
		}
		set
		{
			if (value < 0) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				value, $"{nameof(Quantity)} must be >= 0");
			}

			quantity = value;
		}
	}

	// get dan set harga per barang
	public decimal HargaPerBarang
	{
		get
		{
			return hargaPerBarang;
		}
		set
		{
			if (value < 0) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				  value, $"{nameof(HargaPerBarang)} must be >= 0");
			}

			hargaPerBarang = value;
		}
	}

	// mengembalikan representasi string dari objek tagihan
	public override string ToString() =>
	  $"tagihan:\npart number: {PartNumber} ({PartDescription})\n" +
	  $"quantity: {Quantity}\nharga per barang: {HargaPerBarang:C}";

	// method required to carry out contract with interface IPayable
	public decimal DapatJumlahPembayaran() => Quantity * HargaPerBarang;

}

public abstract class Pegawai : HarusBayar
{
	public string NamaDepan { get; }
	public string NamaBelakang { get; }
	public string NomorKTP { get; }

	// konstruktor tiga parameter
	public Pegawai(string namaDepan, string namaBelakang,
	  string nomorKTP)
	{
		NamaDepan = namaDepan;
		NamaBelakang = namaBelakang;
		NomorKTP = nomorKTP;
	}
	// mengembalikan representasi stirng dari objek Pegawai, menggunakan properti
	public override string ToString() => $"{NamaDepan} {NamaBelakang}\n" +
			$"nomor kartu tanda penduduk: {NomorKTP}";

	// metode abstrak ditimpa oleh kelas turunan
	public abstract decimal Pendapatan(); // tidak ada implementasi disini

	// implementing GetPaymentAmount here enables the entire Employee
	// class hierarchy to be used in an app that processes IPayables
	public decimal DapatJumlahPembayaran() => Pendapatan();
}
public class PegawaiBergaji : Pegawai
{
	private decimal gajiMingguan;

	// konstruktor empat parameter
	public PegawaiBergaji(string namaDepan, string namaBelakang,
	  string nomorKTP, decimal gajiMingguan)
	  : base(namaDepan, namaBelakang, nomorKTP)
	{
		GajiMingguan = gajiMingguan; // memvalidasi gaji
	}

	// get dan set gaji dari pegawai bergaji 
	public decimal GajiMingguan
	{
		get
		{
			return gajiMingguan;
		}
		set
		{
			if (value < 0) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				  value, $"{nameof(GajiMingguan)} must be >= 0");
			}

			gajiMingguan = value;
		}
	}

	// menghitung pendapatan; override abstract method Earnings in Employee

	public override decimal Pendapatan() => GajiMingguan;

	// mengembalikan representasi string dari objek pegawai bergaji
	public override string ToString() =>
	  $"pegawai bergaji: {base.ToString()}\n" +
	  $"gaji mingguan: {GajiMingguan:C}";
}
class TestAntarMukaUntukDibayar
{
	static void Main()
	{
		// membuat List<IPayable> dan menginialisasikannya dengan empat 
		// objek dari kelas yang mengimplentasikan antar muka dari HarusBayar 
		var payableObjects = new List<HarusBayar>() { 
			new Tagihan("01234", "seat", 2, 375.00M), 
			new Tagihan("56789", "tire", 4, 79.95M),
            new PegawaiBergaji("John", "Smith", "111-11-1111", 800.00M),
            new PegawaiBergaji("Lisa", "Barnes", "888-88-8888", 1200.00M)};

		Console.WriteLine(
		"Memproses Tagihan dan Pegawai Secara Polymorphism:\n");

		// generically process each element in payableObjects
		foreach (var payable in payableObjects)
		{
			// output payable and its appropriate payment amount
			Console.WriteLine($"{payable}");
			Console.WriteLine(
			 $"pembayaran: {payable.DapatJumlahPembayaran():C}\n");
		}
	}
}





