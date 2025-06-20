using System;
using System.Collections.Generic;

namespace TeknikServisTanitim.DAL;

public partial class DemoRequest
{
    public int Id { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Eposta { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string? FirmaAdi { get; set; }

    public string? TalepMesaji { get; set; }

    public DateTime? Tarih { get; set; }

    public string? Durum { get; set; }
}
