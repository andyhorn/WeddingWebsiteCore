export const compareTimes = function (timeOne, timeTwo) {
    const timeOneTicks = getTicksFromTimeString(timeOne);
    const timeTwoTicks = getTicksFromTimeString(timeTwo);

    return timeOneTicks > timeTwoTicks
        ? 1
        : timeOneTicks < timeTwoTicks
            ? -1 : 0;
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