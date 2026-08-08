document.addEventListener("DOMContentLoaded", function () {
    const STORAGE_KEY = "guest_identifier";

    let guestIdentifier = localStorage.getItem(STORAGE_KEY);

    if (!guestIdentifier) {
        guestIdentifier = crypto.randomUUID();
        localStorage.setItem(STORAGE_KEY, guestIdentifier);
    }

    fetch(`/Home/GetSessions?guestIdentifier=${guestIdentifier}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById("sessions-container").innerHTML = html;
        });

    const refactorButton = document.getElementById("refactor-button");
    const codeInput = document.querySelector(".code-input");

    if (!refactorButton || !codeInput)
        return;

    refactorButton.addEventListener("click", async () => {
        const prompt = codeInput.value.trim();
        if (!prompt) {
            codeInput.focus();
            return;
        }

        try {
            const promptContent = document.getElementById("prompt-content");
            const responseContent = document.getElementById("response-content");

            promptContent.textContent = prompt;

            responseContent.innerHTML = `<span class="ai-loading"><span class="ai-spinner"></span>Refactoring...</span>`;

            refactorButton.disabled = true;
            refactorButton.textContent = "Refactoring...";

            const response = await fetch("/Refactor", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    prompt: prompt
                })
            });

            if (!response.ok) {
                throw new Error(`Request failed: ${response.status}`);
            }

            const result = await response.json();

            responseContent.textContent = result.response;
        }
        catch (error) {
            console.error("Refactoring failed:", error);
        }
        finally {
            refactorButton.disabled = false;
            refactorButton.textContent = "Refactor";
        }
    });

});

function toggleSidebar() {
    const sidebar = document.getElementById("sidebar");
    sidebar.classList.toggle("open");
}