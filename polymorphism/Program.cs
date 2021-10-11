using System;

public class PegawaiKomisi
{
	public string NamaDepan { get; }
	public string NamaBelakang { get; }
	public string NomorKTP { get; }
	private decimal penjualanKotor; // penjualan kotor mingguan
	private decimal tarifKomisi; // prosentase komisi

	// konstruktor lima parameter
	public PegawaiKomisi(string namaDepan, string namaBelakang,
	  string nomorKTP, decimal penjualanKotor,
	  decimal tarifKomisi)
	{
		// panggilan implisist ke konstruktor objek terjadi disini
		NamaDepan = namaDepan;
		NamaBelakang = namaBelakang;
		NomorKTP = nomorKTP;
		PenjualanKotor = penjualanKotor; // memvalidasi penjualan kotor	
		TarifKomisi = tarifKomisi; // memvalidasi tarif komisi	
	}

	// get dan set penjualan kotor
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

	// get dan set tarif komisi
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

	// menghitung pendapatan 
	public virtual decimal Pendapatan() => TarifKomisi * PenjualanKotor;

	// mengembalikan representasi string dari objek PegawaiKomisi
	public override string ToString() =>
	  $"pegawai komisi: {NamaDepan} {NamaBelakang}\n" +
	  $"nomor kartu tanda penduduk: {NomorKTP}\n" +
      $"penjualan kotor: {PenjualanKotor:C}\n" +
	  $"tarif komisi: {TarifKomisi:F2}";
}

public class PokokDanKomisiPegawai : PegawaiKomisi
{
	private decimal gajiPokok; // gaji pokok per minggu

	// enam parameter konstruktor kelas turunan
	// dengan memanggil ke konstruktor PegawaiKomisi kelas dasar 
	public PokokDanKomisiPegawai(string namaDepan, string namaBelakang,
	string nomorKTP, decimal penjualanKotor,
	decimal tarifKomisi, decimal gajiPokok)
	: base(namaDepan, namaBelakang, nomorKTP,
	penjualanKotor, tarifKomisi)
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
				throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(GajiPokok)} must be >= 0");
			}

			gajiPokok = value;
		}
	}

	// menghitung pendapatan
	public override decimal Pendapatan() => GajiPokok + base.Pendapatan();

	// mengembalikan representasi string dari PokokDanKomisiPegawai
	public override string ToString() =>
	$"nama {base.ToString()}\ngaji pokok: {GajiPokok:C}";
}

class PolymorphismTest
{
	static void Main()
	{
		// tetapkan referensi kelas dasar menjadi variabel kelas dasar
		var pegawaiKomisi = new PegawaiKomisi("Sue", "Jones", "222-22-2222", 10000.00M, .06M);

		// tetapkan referensi kelas turunan menjadi variabel kelas turunan
		var pokokDanKomisiPegawai = new PokokDanKomisiPegawai("Bob", "Lewis", "333-33-3333", 5000.00M, .04M, 300.00M);

		// memanggil ToString dan Pendapatan pada objek kelas dasar
		// menggunakan variabel kelas dasar
		Console.WriteLine(
		  "Panggil ToString PegawaiKomisi and metode Pendapatan " +
		  "dengan referensi kelas dasar ke objek kelas dasar\n");
		Console.WriteLine(pegawaiKomisi.ToString());
		Console.WriteLine($"pendapatan: {pegawaiKomisi.Pendapatan()}\n");

		// aktifkan ToString dan Pendapatan pada objek kelas turunan
		// menggunakan variabel kelas turunan 
		Console.WriteLine(
		  "Panggil PokokDanKomisiPegawai dan" +
		  " metode Pendapatan dengan referensi kelas turunan ke" +
		  " objek kelas turunan\n");
		Console.WriteLine(pokokDanKomisiPegawai.ToString());
		Console.WriteLine(
		$"pendapatan: {pokokDanKomisiPegawai.Pendapatan()}\n");

		// aktifkan ToString dan Pendapatan pada objek kelas turunan
		// menggunakan variabel kelas dasar
		PegawaiKomisi pegawaiKomisi2 = pokokDanKomisiPegawai;
		Console.WriteLine(
		  "Panggil ToString PokokDanKomisiPegawai dan metode " +
		  "Pendapatan dengan referensi kelas dasar ke objek kelas turunan\n");
		Console.WriteLine(pegawaiKomisi2.ToString());
		Console.WriteLine(
		$"pendapatan: {pokokDanKomisiPegawai.Pendapatan()}\n");
	}
}

