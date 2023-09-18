using System;
using System.Collections.Generic;

public class LibraryApp
{
    public static void Main()
    {
        int menu; // Deklarasi tipe data untuk input pada switch case

        string title;
        string author;
        int publicationYear;

        var libraryCatalog = new LibraryCatalog<Book>(); // Proses Instansiasi dari class LibraryCatalog dan ErrorHandler
        var errorHandler = new ErrorHandler();

        Console.WriteLine( //Pembuatan Menu GUI Console supaya lebih interaktif
        @"================================================
        Selamat Datang di Repository Universitas Wakanda
        =================================================
        Silahkan Pilih Menu:
        1. Tambah Buku
        2. Hapus Buku
        3. Cari Buku berdasarkan Judul
        4. Tampilkan Semua Buku
        0. Exit
        ==================================================");

        while (int.TryParse(Console.ReadLine(), out menu)) // Membuat pengondisian untuk memilih menu menggunakan switch case
        {
            switch (menu)
            {
                case 1: // Kondisi 1 untuk memasukan data buku
                    Console.WriteLine("Masukan buku yang akan ditambahkan:");
                    title = Console.ReadLine();
                    author = Console.ReadLine();
                    if (int.TryParse(Console.ReadLine(), out publicationYear))
                    {
                        Book book = new Book(title, author, publicationYear);
                        libraryCatalog.AddBook(book);
                        Console.WriteLine("Buku Berhasil Ditambahkan");
                    }
                    else
                    {
                        errorHandler.ErrorAddBook();
                    }
                    break;
                case 2: // Kondisi 2 untuk menghapus data buku
                    Console.WriteLine("Masukan judul buku yang akan dihapus:");
                    title = Console.ReadLine();
                    Book bookToRemove = libraryCatalog.FindBook(title);

                    if (bookToRemove != null)
                    {
                        libraryCatalog.RemoveBook(bookToRemove); // Remove a book by passing the book object
                        Console.WriteLine("Buku Berhasil Dihapus");
                    }
                    else
                    {
                        errorHandler.ErrorRemoveBook();
                    }
                    break;
                case 3: // Kondisi 3 untuk mencari buku yang diinginkan berdasarkan judul buku
                    Console.WriteLine("Silahkan Masukan judul:");
                    title = Console.ReadLine();

                    Book findBook = libraryCatalog.FindBook(title); // Call FindBook method on libraryCatalog

                    if (findBook != null)
                    {
                        Console.WriteLine("Buku ditemukan:");
                        Console.WriteLine(findBook);
                    }
                    else
                    {
                        errorHandler.ErrorFindBook();
                    }
                    break;
                case 4: // Kondisi 3 untuk menampilkan semua buku yang tersimpan
                    Console.WriteLine("Menampilkan Semua Buku:");

                    if (libraryCatalog.ListBooks().Count > 0)
                    {
                        foreach (var displayedBook in libraryCatalog.ListBooks())
                        {
                            Console.WriteLine(displayedBook);
                        }
                    }
                    else
                    {
                        errorHandler.ErrorListBook();
                    }
                    break;
                case 0: // Kondisi 5 untuk menghentikan program
                    Environment.Exit(0); // Keluar dari program
                    break;
                default: // Kondisi lain apabila users tidak memasukan nomer perintah yang sesuai
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}

public class Book // Membuat class Book sebagai BluePrint menyimpan data buku
{
    public string Title { get; set; } // deklarasi atribut yang dibutuhkan untuk data buku
    public string Author { get; set; }
    public int PublicationYear { get; set; }

    public Book(string title, string author, int publicationYear) // Konstruktor dari class book yang berisi atribut data buku
    {
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
    }

    public override string ToString() // Method untuk menampilkan data buku
    {
        return $"Title: {Title}\nAuthor: {Author}\nPublication Year: {PublicationYear}\n";
    }
}

public class LibraryCatalog<T> where T : Book // class LibraryCatalog untuk melakukan CRUD pada data buku
{
    private List<T> books = new List<T>(); // List untuk menyimpan daftar data buku

    public void AddBook(T book) // Method untuk menambahkan data buku
    {
        books.Add(book); // Add a book to the catalog
    }

    public void RemoveBook(T book) // Method untuk menghapus data buku
    {
        books.Remove(book);
    }

    public T FindBook(string title) // Method untuk menemukan data buku berdasarkan judul buku
    {
        return books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // Find a book by title
    }

    public List<T> ListBooks() // method untuk menampilkan semua data buku
    {
        return books;
    }
}

public class ErrorHandler // class ErrorHandler untuk menangani error pada program
{
    public void ErrorAddBook() // method untuk menangani error ketika menambahkan data buku
    {
        Console.WriteLine($"Tidak Dapat Menambahkan Buku, Silahkan Masukan Input Dengan Benar");
    }

    public void ErrorRemoveBook() //  method untuk menangani error ketika menghapus data buku
    {
        Console.WriteLine($"Tidak Dapat Menghapus Buku, Silahkan Pilih Buku Dengan Benar");
    }

    public void ErrorFindBook() // method untuk menangani error ketika buku yang dicari tidak ditemukan
    {
        Console.WriteLine("Buku tidak ditemukan.");
    }

    public void ErrorListBook() // method untuk menangani error ketika buku yang ditampilkan tidak ditemukan
    {
        Console.WriteLine("List Buku Kosong");
    }
}
