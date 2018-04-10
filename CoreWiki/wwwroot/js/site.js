// Write your Javascript code.
(function () {

	var reformatTimeStamps = function () {

		var timeStamps = document.getElementsByClassName("timeStampValue");
		for (var ts of timeStamps) {

			var thisTimeStamp = ts.getAttribute("data-value");
			var date = new Date(thisTimeStamp);
			ts.textContent = moment(date).format('LLL');

		}

	}

	reformatTimeStamps();

})();