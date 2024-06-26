﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TerrabitTest.Data;

public partial class BankStatementHistory
{
    public Guid Id { get; set; }

    public Guid BankAccountId { get; set; }

    public int StatementType { get; set; }

    public decimal Balance { get; set; }

    public Guid? UserIdTransferTo { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual BankAccount BankAccount { get; set; }
}