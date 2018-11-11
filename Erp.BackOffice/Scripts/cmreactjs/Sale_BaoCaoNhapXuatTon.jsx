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
 var WarehouseId = $('#WarehouseId').val();  
 var CategoryCode = $('#CategoryCode').val();  
 var ProductGroup = $('#ProductGroup').val();  
 var Manufacturer = $('#Manufacturer').val();  

        $.getJSON(url, {
        StartDate: StartDate,  
EndDate: EndDate,  
WarehouseId: WarehouseId,  
CategoryCode: CategoryCode,  
ProductGroup: ProductGroup,  
Manufacturer: Manufacturer

        }).done(function (data) {
            ReactDOM.render(
            <TableReport datatable={data} />,
        document.getElementById('react_report')
          );
            });


      var TableReport = React.createClass({
 render: function() {
 
 sumPrice=0;
 sumFirst_Remain=0;
 sumLast_InboundQuantity=0;
 sumLast_OutboundQuantity=0;
 sumRemain=0;
 var tbody = this.props.datatable.map(function(comment_obj, i) {
 
 sumPrice += comment_obj.Price;
 sumFirst_Remain += comment_obj.First_Remain;
 sumLast_InboundQuantity += comment_obj.Last_InboundQuantity;
 sumLast_OutboundQuantity += comment_obj.Last_OutboundQuantity;
 sumRemain += comment_obj.Remain;
   return (
  <BodyTable stt ={i + 1}
 
CategoryCode= { comment_obj.CategoryCode }
ProductCode= { comment_obj.ProductCode }
ProductName= { comment_obj.ProductName }
WarehouseName= { comment_obj.WarehouseName }
Price= { comment_obj.Price }
ProductUnit= { comment_obj.ProductUnit }
First_Remain= { comment_obj.First_Remain }
Last_InboundQuantity= { comment_obj.Last_InboundQuantity }
Last_OutboundQuantity= { comment_obj.Last_OutboundQuantity }
Remain= { comment_obj.Remain }
   key ={i}>
    </BodyTable>
   );
    });
  return (
   <table className ="table table-bordered table-responsive" id = "cTable">
 <thead>
  <tr>
  <th colSpan = "11" className ="cell-center">< h4 >{ title} từ { startdate} đến {enddate}</h4></th>
  </tr>
  <tr>
 <th> STT </th>
 
 <th className="text-center">Danh mục sản phẩm</th>
 <th className="text-center">Mã sản phẩm</th>
 <th className="text-center">Tên sản phẩm</th>
 <th className="text-center">Kho</th>
 <th className="text-center">Đơn giá</th>
 <th className="text-center">Đơn vị tính</th>
 <th className="text-center">Tồn đầu kỳ</th>
 <th className="text-center">Nhập trong kỳ</th>
 <th className="text-center">Xuất trong kỳ</th>
 <th className="text-center">Tổng tồn cuối kỳ</th>
  </tr>
   </thead>
  <tbody>
  {tbody}
  <tr className="tr-bold">
  <td></td>
  
 <td className ="cell-center text-right"></td> 
 <td className ="cell-center text-right"></td> 
 <td className ="cell-center text-right"></td> 
 <td className ="cell-center text-right"></td> 
   <td className="text-right">{ConvertSoAm(sumPrice)}</td>
 <td className ="cell-center text-right"></td> 
   <td className="text-right">{ConvertSoAm(sumFirst_Remain)}</td>
   <td className="text-right">{ConvertSoAm(sumLast_InboundQuantity)}</td>
   <td className="text-right">{ConvertSoAm(sumLast_OutboundQuantity)}</td>
   <td className="text-right">{ConvertSoAm(sumRemain)}</td>
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
    var color_Price = parseInt(this.props.Price) < 0 ? "text-right red":"text-right green"; 
  var color_First_Remain = parseInt(this.props.First_Remain) < 0 ? "text-right red":"text-right green"; 
  var color_Last_InboundQuantity = parseInt(this.props.Last_InboundQuantity) < 0 ? "text-right red":"text-right green"; 
  var color_Last_OutboundQuantity = parseInt(this.props.Last_OutboundQuantity) < 0 ? "text-right red":"text-right green"; 
  var color_Remain = parseInt(this.props.Remain) < 0 ? "text-right red":"text-right green"; 

  return (
   <tr className ={name}>
  <td>{this.props.stt}</td>
   
  <td>{this.props.CategoryCode}</td>
  <td>{this.props.ProductCode}</td>
  <td>{this.props.ProductName}</td>
  <td>{this.props.WarehouseName}</td>
   <td className ={color_Price}>{ConvertSoAm(this.props.Price)}</td>
  <td>{this.props.ProductUnit}</td>
   <td className ={color_First_Remain}>{ConvertSoAm(this.props.First_Remain)}</td>
   <td className ={color_Last_InboundQuantity}>{ConvertSoAm(this.props.Last_InboundQuantity)}</td>
   <td className ={color_Last_OutboundQuantity}>{ConvertSoAm(this.props.Last_OutboundQuantity)}</td>
   <td className ={color_Remain}>{ConvertSoAm(this.props.Remain)}</td>
   </ tr >
   );
   }
   });