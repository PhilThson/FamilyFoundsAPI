﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyFoundsApi.Domain;

public class Transaction : BaseEntity<long>
{
    [Required]
    public double Amount { get; set; }
    [Required]
    [StringLength(256)]
    public string Contractor { get; set; }
    [StringLength(256)]
    public string Title { get; set; }
    [StringLength(1024)]
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
    public short? CategoryId { get; set; }
    public short? ImportSourceId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    [ForeignKey(nameof(ImportSourceId))]
    public virtual ImportSource ImportSource { get; set; }

}
