document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("audio").forEach(audio => {
        audio.addEventListener("loadedmetadata", () => {
            const duration = audio.duration;
            const min = Math.floor(duration / 60);
            const sec = Math.floor(duration % 60).toString().padStart(2, "0");

            audio.parentElement.querySelector(".song-duration")
                .innerText = `⏱ ${min}:${sec}`;
        });
    });
});
