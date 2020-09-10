export const compareTimes = function (timeOne, timeTwo) {
    const timeOneTicks = getTicksFromTimeString(timeOne);
    const timeTwoTicks = getTicksFromTimeString(timeTwo);

    return timeOneTicks > timeTwoTicks
        ? 1
        : timeOneTicks < timeTwoTicks
            ? -1 : 0;
}

export const compareDates = function (dateOne, dateTwo) {
    if (!(dateOne instanceof Date)) {
        dateOne = new Date(dateOne);
    }

    if (!(dateTwo instanceof Date)) {
        dateTwo = new Date(dateTwo);
    }

    const zeroTime = function (date) {
        const alteredDate = new Date(date);
        alteredDate.setHours(0);
        alteredDate.setMinutes(0);
        alteredDate.setSeconds(0);
        alteredDate.setMilliseconds(0);
        return alteredDate;
    }

    const strictDateOne = zeroTime(dateOne);
    const strictDateTwo = zeroTime(dateTwo);

    const ticksOne = strictDateOne.getTime();
    const ticksTwo = strictDateTwo.getTime();

    return ticksOne > ticksTwo
        ? 1
        : ticksOne < ticksTwo
            ? -1
            : 0;
}

export const parseDateString = function (dateString) {
    const dateSection = dateString.split("T")[0];
    const timeSection = dateString.split("T")[1];

    const year = dateSection.split("-")[0];
    const month = dateSection.split("-")[1];
    const day = dateSection.split("-")[2];

    const hour = timeSection.split(":")[0];
    const minute = timeSection.split(":")[1];
    const second = timeSection.split(":")[2];

    const ticks = Date.UTC(year, month, day, hour, minute, second, 0);
    const date = new Date(ticks);

    return date;
}

const getTicksFromTimeString = (timeString) => {
    let ticks = 0;
    const getTicksFromHours = getTickMultiplier(1000 * 60 * 60);
    const getTicksFromMinutes = getTickMultiplier(1000 * 60);
    const getTicksFromSeconds = getTickMultiplier(1000);

    if (timeString.includes(":")) {
        const timeSplit = timeString.split(":");

        if (timeSplit.length > 0) {
            ticks += getTicksFromHours(timeSplit[0]);
        }
        if (timeSplit.length > 1) {
            ticks += getTicksFromMinutes(timeSplit[1]);
        }
        if (timeSplit.length > 2) {
            ticks += getTicksFromSeconds(timeSplit[2]);
        }
        if (timeSplit.length > 3) {
            ticks += timeSplit[3];
        }
    } else {
        // assume only the hour value has been passed
        const hours = parseInt(timeString);
        ticks += getTicksFromHours(hours);
    }

    return ticks;
}

const getTickMultiplier = (multiplier) => (val) => {
    return multiplier * parseInt(val);
}