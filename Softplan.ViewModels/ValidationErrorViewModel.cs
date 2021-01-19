using System;
using System.Collections.Generic;

namespace Softplan.ViewModels
{
  public class ValidationErrorsViewModel
  {
    public string Title { get; set; }
    public List<Detail> Details { get; set; }
    public DateTime Date { get; set; }
  }
  public class ErrorViewModel
  {
    public string Title { get; set; }
    public Detail Detail { get; set; }
    public DateTime Date { get; set; }
  }
  public class Detail
  {
    public string Code { get; set; }
    public string Message { get; set; }
  }
}