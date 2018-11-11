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
      var BranchId = $('#BranchId').val();  
 var WarehouseId = $('#WarehouseId').val();  
 var ProductGroup = $('#ProductGroup').val();  
 var CategoryCode = $('#CategoryCode').val();  
 var Manufacturer = $('#Manufacturer').val();  

        $.getJSON(url, {
        BranchId: BranchId,  
WarehouseId: WarehouseId,  
ProductGroup: ProductGroup,  
CategoryCode: CategoryCode,  
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
 sumQuantity=0;
 var tbody = this.props.datatable.map(function(comment_obj, i) {
 
 sumPrice += comment_obj.Price;
 sumQuantity += comment_obj.Quantity;
   return (
  <BodyTable stt ={i + 1}
 
CategoryCode= { comment_obj.CategoryCode }
Manufacturer= { comment_obj.Manufacturer }
ProductCode= { comment_obj.ProductCode }
ProductName= { comment_obj.ProductName }
WarehouseName= { comment_obj.WarehouseName }
Price= { comment_obj.Price }
ProductUnit= { comment_obj.ProductUnit }
Quantity= { comment_obj.Quantity }
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
 
 <th className="text-center">Danh mục sản phẩm</th>
 <th className="text-center">Nhà sản xuất</th>
 <th className="text-center">Mã sản phẩm</th>
 <th className="text-center">Tên sản phẩm</th>
 <th className="text-center">Kho</th>
 <th className="text-center">Đơn giá</th>
 <th className="text-center">Đơn vị tính</th>
 <th className="text-center">Số lượng</th>
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
   <td className="text-right">{ConvertSoAm(sumPrice)}</td>
 <td className ="cell-center text-right"></td> 
   <td className="text-right">{ConvertSoAm(sumQuantity)}</td>
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
  var color_Quantity = parseInt(this.props.Quantity) < 0 ? "text-right red":"text-right green"; 

  return (
   <tr className ={name}>
  <td>{this.props.stt}</td>
   
  <td>{this.props.CategoryCode}</td>
  <td>{this.props.Manufacturer}</td>
  <td>{this.props.ProductCode}</td>
  <td>{this.props.ProductName}</td>
  <td>{this.props.WarehouseName}</td>
   <td className ={color_Price}>{ConvertSoAm(this.props.Price)}</td>
  <td>{this.props.ProductUnit}</td>
   <td className ={color_Quantity}>{ConvertSoAm(this.props.Quantity)}</td>
   </ tr >
   );
   }
   });