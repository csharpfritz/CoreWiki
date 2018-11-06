// Write your Javascript code.
(function () {

	var reformatTimeStamps = function () {

		var timeStamps = document.querySelectorAll(".timeStampValue");
		for (var ts of timeStamps) {

			var thisTimeStamp = ts.getAttribute("data-value");
			var date = new Date(thisTimeStamp);
			moment.locale(window.navigator.userLanguage || window.navigator.language);
			ts.textContent = moment(date).format('LLL');

		}

	}

	reformatTimeStamps();

})();

//Prevent duplicate submit

$("form").submit(function (e) {
	if ($(this).valid()) {
		if ($(this).attr('attempted') === 'true') {
			e.preventDefault();
		}
		else {
			$(this).attr('attempted', 'true');
		}
	}
});
