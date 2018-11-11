
/* global Chart */

window.chartColors = {
    red: '#f83a22',
    orange: '#ffad46',
    yellow: '#fbe983',
    green: '#16a765',
    lochinvar: '#ac725e',
    blue: '#4986e7',
    purple: '#a47ae2',
    grey: '#c2c2c2',
    dusty_grey: '#555',
    pink: '#f691b2',
    cyan: '#9fc6e7'
};

window.randomScalingFactor = function () {
    return (Math.random() > 0.5 ? 1.0 : -1.0) * Math.round(Math.random() * 100);
};

window.MONTHS_EN = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
window.MONTHS_VN = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"];