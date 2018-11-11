function ConvertSoAm(input_date) {
    var convertdate = "";
    if (parseInt(input_date) < 0) {
        var aa = parseInt(input_date) * (-1);
        var bb = aa.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
        convertdate = "(" + bb + ")";
    }
    else {
        convertdate = parseInt(input_date).toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
    }
    return convertdate;
}
function GetDatatable(url, title) {
      var StartDate = $('#StartDate').val();  
 var EndDate = $('#EndDate').val();  
 var BranchId = $('#BranchId').val();  
 var SalerId = $('#SalerId').val();  

        $.getJSON(url, {
        StartDate: StartDate,  
EndDate: EndDate,  
BranchId: BranchId,  
SalerId: SalerId

        }).done(function (data) {
            ReactDOM.render(
            <TableReport datatable={data} />,
        document.getElementById('react_report')
          );
            });


      var TableReport = React.createClass({
 render: function() {
 
 sumTotalAmount=0;
 sumcnt=0;
 sumdaytra=0;
 var tbody = this.props.datatable.map(function(comment_obj, i) {
 
 sumTotalAmount += comment_obj.TotalAmount;
 sumcnt += comment_obj.cnt;
 sumdaytra += comment_obj.daytra;
   return (
  <BodyTable stt ={i + 1}
 
MaChungTuGoc= { comment_obj.MaChungTuGoc }
CustomerName= { comment_obj.CustomerName }
TotalAmount= { comment_obj.TotalAmount }
cnt= { comment_obj.cnt }
NextPaymentDate= { comment_obj.NextPaymentDate }
daytra= { comment_obj.daytra }
SalerFullName= { comment_obj.SalerFullName }
   key ={i}>
    </BodyTable>
   );
    });
  return (
   <table className ="table table-bordered table-responsive" id = "cTable">
 <thead>
  <tr>
  <th colSpan = "9" className ="cell-center">< h4 >{ title} từ { startdate} đến {enddate}</h4></th>
  </tr>
  <tr>
 <th> STT </th>
 
 <th className="text-center">Số hóa đơn</th>
 <th className="text-center">Khách hàng</th>
 <th className="text-center">Trị giá công nợ gốc</th>
 <th className="text-center">Trị giá còn phải thu</th>
 <th className="text-center">Ngày phải thu</th>
 <th className="text-center">Số ngày quá hạn</th>
 <th className="text-center">Tên nhân viên</th>
  </tr>
   </thead>
  <tbody>
  {tbody}
  <tr className="tr-bold">
  <td></td>
  
 <td className ="cell-center text-right"></td> 
 <td className ="cell-center text-right"></td> 
   <td className="text-right">{ConvertSoAm(sumTotalAmount)}</td>
   <td className="text-right">{ConvertSoAm(sumcnt)}</td>
 <td className ="cell-center text-right"></td> 
   <td className="text-right">{ConvertSoAm(sumdaytra)}</td>
 <td className ="cell-center text-right"></td> 
   </tr>
    </tbody>
    </table>
     );
     }
     });
     };

     
  var BodyTable = React.createClass({
    render: function() {
     var name = this.props.stt % 2 == 0 ? "alert-warning" : "";
    var color_TotalAmount = parseInt(this.props.TotalAmount) < 0 ? "text-right red":"text-right green"; 
  var color_cnt = parseInt(this.props.cnt) < 0 ? "text-right red":"text-right green"; 
  var color_daytra = parseInt(this.props.daytra) < 0 ? "text-right red":"text-right green"; 

  return (
   <tr className ={name}>
  <td>{this.props.stt}</td>
   
  <td>{this.props.MaChungTuGoc}</td>
  <td>{this.props.CustomerName}</td>
   <td className ={color_TotalAmount}>{ConvertSoAm(this.props.TotalAmount)}</td>
   <td className ={color_cnt}>{ConvertSoAm(this.props.cnt)}</td>
  <td>{this.props.NextPaymentDate}</td>
   <td className ={color_daytra}>{ConvertSoAm(this.props.daytra)}</td>
  <td>{this.props.SalerFullName}</td>
   </ tr >
   );
   }
   });