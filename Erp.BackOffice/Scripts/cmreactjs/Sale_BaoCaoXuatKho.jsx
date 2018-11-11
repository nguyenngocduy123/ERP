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
      var CategoryCode = $('#CategoryCode').val();  
     var Manufacturer = $('#Manufacturer').val();  
     var WarehouseId = $('#WarehouseId').val();  
     var StartDate = $('#StartDate').val();  
     var EndDate = $('#EndDate').val();  
     var ProductGroup = $('#ProductGroup').val();
        $.getJSON(url, {
        CategoryCode: CategoryCode, 
		ProductGroup:ProductGroup, 
		Manufacturer: Manufacturer,  
		WarehouseId: WarehouseId,  
		StartDate: StartDate,
		EndDate:EndDate
        }).done(function (data) {
            ReactDOM.render(
            <TableReport datatable={data}/>,
        document.getElementById('react_report')
          );
            });


      var TableReport = React.createClass({
 render: function() {
 sum_total=0;
 suminvoice=0;
 sumservice=0;
 suminternal=0;
 var tbody = this.props.datatable.map(function(comment_obj, i) {
 
 suminvoice += comment_obj.invoice;
 sumservice += comment_obj.service;
 suminternal += comment_obj._internal;
   return (
  <BodyTable stt ={i + 1}
 
ProductCode= { comment_obj.ProductCode }
ProductName= { comment_obj.ProductName }
CategoryCode= { comment_obj.CategoryCode }
Manufacturer= { comment_obj.Manufacturer }
WarehouseName= { comment_obj.WarehouseName }
Unit= { comment_obj.Unit }
Price= { comment_obj.Price }
invoice= { comment_obj.invoice }
service= { comment_obj.service }
internal= { comment_obj._internal }
   key ={i}>
    </BodyTable>
   );
    });
  return (
   <table className ="table table-bordered table-responsive" id = "cTable">
 <thead>
  <tr>
  <th colSpan = "11" className ="cell-center">< h4 >{title} từ {StartDate} đến {EndDate}</h4></th>
  </tr>
  <tr>
 <th> STT </th>
 <th className="text-center">Mã sản phẩm</th>
 <th className="text-center">Tên sản phẩm</th>
 <th className="text-center">Danh mục sản phẩm</th>
 <th className="text-center">Nhà sản xuất</th>
 <th className="text-center">Tên kho</th>
 <th className="text-center">Đơn vị tính</th>
 <th className="text-center">Giá sản phẩm</th>
 <th className="text-center">Xuất bán</th>
 <th className="text-center">Xuất sử dụng</th>
 <th className="text-center">Xuất chuyển</th>
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
 <td className ="cell-center text-right"></td> 
 <td className ="cell-center text-right"></td> 
   <td className="text-right">{ConvertSoAm(sumPrice)}</td>
   <td className="text-right">{ConvertSoAm(suminvoice)}</td>
   <td className="text-right">{ConvertSoAm(sumservice)}</td>
   <td className="text-right">{ConvertSoAm(suminternal)}</td>
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
  var color_invoice = parseInt(this.props.invoice) < 0 ? "text-right red":"text-right green"; 
  var color_service = parseInt(this.props.service) < 0 ? "text-right red":"text-right green"; 
  var color_internal = parseInt(this.props._internal) < 0 ? "text-right red":"text-right green"; 

  return (
   <tr className ={name}>
  <td>{this.props.stt}</td>
  <td>{this.props.ProductCode}</td>
  <td>{this.props.ProductName}</td>
  <td>{this.props.CategoryCode}</td>
  <td>{this.props.Manufacturer}</td>
  <td>{this.props.WarehouseName}</td>
  <td>{this.props.Unit}</td>
   <td className ={color_Price}>{ConvertSoAm(this.props.Price)}</td>
   <td className ={color_invoice}>{ConvertSoAm(this.props.invoice)}</td>
   <td className ={color_service}>{ConvertSoAm(this.props.service)}</td>
   <td className ={color_internal}>{ConvertSoAm(this.props._internal)}</td>
   </ tr >
   );
   }
   });