function BlockUI()
{
    $.blockUI({
        message: '<h1>Xin vui lòng chờ xử lý...</h1>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            opacity: .5,
            color: '#fff'
        }
    });
}

function UnblockUI()
{
    $.unblockUI();
}