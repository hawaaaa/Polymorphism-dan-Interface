using System;
using System.Collections.Generic;

public abstract class Pegawai
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

	//	menggembalikan representasi string dari objek Pegawai, menggunakan properti
	public override string ToString() => $"{NamaDepan} {NamaBelakang}\n" +
		$"nomor kartu tanda penduduk: {NomorKTP}";

	// metode abstrak diganti oleh kelas turunan
	public abstract decimal Pendapatan(); // tidak ada implementasi di sini
}

public class PegawaiBergaji : Pegawai
{
	private decimal gajiMingguan;

	// konstrtuktor empat parameter
	public PegawaiBergaji(string namaDepan, string namaBelakang,
	string nomorKTP, decimal gajiMingguan)
		: base(namaDepan, namaBelakang, nomorKTP)
	{
		GajiMingguan = gajiMingguan; // memvalidasi gaji
	}

	// get dan set gaji mingguan
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
	// menghitung pendapatan; override metode abstrak Pendapatan pada Pegawai
	public override decimal Pendapatan() => GajiMingguan;

	// mengembalikan string representasi dari objek PegawaiBergaji
	public override string ToString() =>
	$"pegawai bergaji: {base.ToString()}\n" +
	$"gaji mingguan: {GajiMingguan:C}";
}
public class PegawaiPerJam : Pegawai
{
	private decimal upah; // upah per jam
	private decimal jam; // jam kerja selama minggu

	// konstruktor lima parameter
	public PegawaiPerJam(string namaDepan, string namaBelakang,
	string nomorKTP, decimal upahPerJam,
	decimal jamKerja)
		: base(namaDepan, namaBelakang, nomorKTP)
	{
		Upah = upahPerJam; // memvalidasi upah per jam
		Jam = jamKerja; // memvalidasi jam kerja
	}

	//  get dan set upah dari pegawai perjam
	public decimal Upah
	{
		get
		{
			return upah;
		}
		set
		{
			if (value < 0) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				value, $"{nameof(Upah)} must be >= 0");
			}

			upah = value;
		}
	}
	// get dan set jam dari pegawai perjam
	public decimal Jam
	{
		get
		{
			return jam;
		}
		set
		{
			if (value < 0 || value > 168) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				value, $"{nameof(Jam)} must be >= 0 and <= 168");
			}

			jam = value;
		}
	}
	// menghitung pendapatan; override metode abstrak Pendapatan pada Pegawai
	public override decimal Pendapatan()
	{
		if (Jam <= 40) // no overtime
		{
			return Upah * Jam;
		}
		else
		{
			return (40 * Upah) + ((Jam - 40) * Upah * 1.5M);
		}
	}

	// mengembalikan representasi stirng dari objek PegawaiPerjam 
	public override string ToString() =>
	$"pegawai perjam: {base.ToString()}\n" +
	$"upah perjam: {Upah:C}\njam kerja: {Jam:F2}";
}

public class PegawaiKomisi : Pegawai
{
	private decimal penjualanKotor; // penjualan kotor mingguan
	private decimal tarifKomisi; // prosentase komisi

	// konstrukor lima parameter
	public PegawaiKomisi(string namaDepan, string namaBelakang,
	string nomorKTP, decimal penjualanKotor,
	decimal tarifKomisi)
		: base(namaDepan, namaBelakang, nomorKTP)
	{
		PenjualanKotor = penjualanKotor; // memvalidasi penjualan kotor
		TarifKomisi = tarifKomisi; // memvalidasi taris komisi
	}

	//  get dan set penjualan kotor dari pegawai komisi
	public decimal PenjualanKotor
	{
		get
		{
			return penjualanKotor;
		}
		set
		{
			if (value < 0) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				value, $"{nameof(PenjualanKotor)} must be >= 0");
			}

			penjualanKotor = value;
		}
	}

	// get dan set tarif komisi dari pegawai komisi
	public decimal TarifKomisi
	{
		get
		{
			return tarifKomisi;
		}
		set
		{
			if (value <= 0 || value >= 1) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				value, $"{nameof(TarifKomisi)} must be > 0 and < 1");
			}

			tarifKomisi = value;
		}
	}

	// menghitung pendapatan; override metode abstrak pendapatan di pegawai
	public override decimal Pendapatan() => TarifKomisi * PenjualanKotor;

	// mengembalikan representasi string dari objek PegawaiKomisi
	public override string ToString() =>
			$"pegawai komisi: {base.ToString()}\n" +
			$"penjualan kotor: {PenjualanKotor:C}\n" +
			$"tarif komisi: {TarifKomisi:F2}";
}
public class PokokDanKomisiPegawai : PegawaiKomisi
{
	private decimal gajiPokok; // gaji pokok per minggu

	// konstruktor enam parameter
	public PokokDanKomisiPegawai(string namaDepan, string namaBelakang,
	string nomorKTP, decimal penjualanKotor,
	decimal tarifKomisi, decimal gajiPokok)
	: base(namaDepan, namaBelakang, nomorKTP, penjualanKotor, tarifKomisi)
	{
		GajiPokok = gajiPokok; // memvalidasi gaji pokok	
	}

	public decimal GajiPokok
	{
		get
		{
			return gajiPokok;
		}
		set
		{
			if (value < 0) // validasi
			{
				throw new ArgumentOutOfRangeException(nameof(value),
				value, $"{nameof(GajiPokok)} must be >= 0");
			}

			gajiPokok = value;
		}
	}

	// menghitung pendapatan
	public override decimal Pendapatan() => GajiPokok + base.Pendapatan();

	// mengembalikan string representasi dari PokokDanKomisiPegawai
	public override string ToString() =>
	$"nama {base.ToString()}\ngaji pokok: {GajiPokok:C}";
}

class TestSistemPenggajian
{
	static void Main()
	{
		// membuat objek kelas turunan
		var salariedEmployee = new PegawaiBergaji("John", "Smith", "111-11-1111", 800.00M);
		var hourlyEmployee = new PegawaiPerJam("Karen", "Price", "222-22-2222", 16.75M, 40.0M);
		var commissionEmployee = new PegawaiKomisi("Sue", "Jones", "333-33-3333", 10000.00M, .06M);
		var basePlusCommissionEmployee = new PokokDanKomisiPegawai("Bob", "Lewis", "444-44-4444", 5000.00M, .04M, 300.00M);


		Console.WriteLine("Memproses Pegawai Secara Individual:\n");

		Console.WriteLine($"{salariedEmployee}\npendapatan: " +
		  $"{salariedEmployee.Pendapatan():C}\n");
		Console.WriteLine(
		  $"{hourlyEmployee}\npendapatan: {hourlyEmployee.Pendapatan():C}\n");
		Console.WriteLine($"{commissionEmployee}\npendapatan: " +
		  $"{commissionEmployee.Pendapatan():C}\n");
		Console.WriteLine($"{basePlusCommissionEmployee}\npendapatan: " +
		  $"{basePlusCommissionEmployee.Pendapatan():C}\n");

		// membuat List<Pegawai> dan menginialisasi dengan objek pegawai
		var employees = new List<Pegawai>() { salariedEmployee, hourlyEmployee, commissionEmployee, basePlusCommissionEmployee };

		Console.WriteLine("Memproses Pegawai Secara Polymorphism:\n");

		// memproses secara general setiap elemen dalam pegawai
		foreach (var currentEmployee in employees)
		{
			Console.WriteLine(currentEmployee); // panggil ToString

			// tentukan apakah unsur merupakan PokokDanKomisiPegawai
			if (currentEmployee is PokokDanKomisiPegawai)
			{
				var employee = (PokokDanKomisiPegawai)currentEmployee;

				employee.GajiPokok *= 1.10M;
				Console.WriteLine("gaji pokok baru dengan kenaikan 10%: " +
				$"{employee.GajiPokok:C}");
			}

			Console.WriteLine($"pendapatan: {currentEmployee.Pendapatan():C}\n");
		}

		// dapatkan tipe nama dari setiap objek di employees
		for (int j = 0; j < employees.Count; j++)
		{
			Console.WriteLine(
			$"Pegawai {j} adalah {employees[j].GetType()}");
		}
	}
}

