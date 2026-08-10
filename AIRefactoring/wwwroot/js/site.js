let currentSessionId = null;

document.addEventListener("DOMContentLoaded", async () => {
    const STORAGE_KEY = "guest_identifier";

    const refactorButton = document.getElementById("refactor-button");
    const codeInput = document.getElementById("code-input");

    if (!refactorButton || !codeInput)
        return;

    const guestIdentifier = getGuestIdentifier();

    currentSessionId = document.getElementById("UserSession_Id")?.value || null;

    await loadSessions(guestIdentifier);

    refactorButton.addEventListener("click", () =>
        refactorCode(guestIdentifier, refactorButton, codeInput)
    );
});


function getGuestIdentifier() {
    const STORAGE_KEY = "guest_identifier";

    let guestIdentifier = localStorage.getItem(STORAGE_KEY);

    if (!guestIdentifier) {
        guestIdentifier = crypto.randomUUID();
        localStorage.setItem(STORAGE_KEY, guestIdentifier);
    }

    return guestIdentifier;
}


async function loadSessions(guestIdentifier) {
    const sessionsContainer = document.getElementById("sessions-container");

    if (!sessionsContainer)
        return;

    try {
        const response = await fetch(
            `/Home/GetSessions?guestIdentifier=${guestIdentifier}&userSessionId=${currentSessionId || ""}`
        );

        if (!response.ok) {
            throw new Error(`Failed to load sessions: ${response.status}`);
        }

        const html = await response.text();

        sessionsContainer.innerHTML = html;
    }
    catch (error) {
        console.error("Failed to load sessions:", error);
    }
}


async function refactorCode(guestIdentifier, refactorButton, codeInput) {
    const prompt = codeInput.value.trim();

    if (!prompt) {
        codeInput.focus();
        return;
    }

    try {
        setRefactorButtonState(refactorButton, true);

        const messageGroup = addConversation(prompt, "Refactoring...", null);

        if (!messageGroup)
            return;

        const response = await fetch("/Refactor", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                prompt,
                userSessionId: currentSessionId,
                guestIdentifier
            })
        });

        if (!response.ok) {
            throw new Error(`Request failed: ${response.status}`);
        }

        const result = await response.json();

        if (!currentSessionId) {
            currentSessionId = result.userSessionId;

            replaceURLParam(currentSessionId);
        }

        updateConversationResponse(messageGroup, result.response, result.createdAt);

        addSessionToSidebar(result.userSessionId, result.userSessionTitle);

        codeInput.value = "";

        messageGroup.scrollIntoView({
            behavior: "smooth",
            block: "start"
        });
    }
    catch (error) {
        console.error("Refactoring failed:", error);
    }
    finally {
        setRefactorButtonState(refactorButton, false);
    }
}


function addConversation(prompt, response, createdAt) {
    const conversation = document.getElementById("conversation");

    if (!conversation)
        return null;

    const messageGroup = document.createElement("div");

    messageGroup.className = "message-group";

    const formattedDate = createdAt ? formatDate(createdAt) : "";

    messageGroup.innerHTML = `
        <div class="message user-message">

            <div class="message-header">
                <span class="message-label">You</span>
                <span class="message-date">
                    ${formattedDate}
                </span>
            </div>

            <div class="message-content"></div>

        </div>

        <div class="message ai-message">

            <div class="message-header">
                <span class="message-label">
                    AI Refactoring
                </span>
            </div>

            <pre class="message-content code-response"></pre>

        </div>
    `;

    const promptContent = messageGroup.querySelector(".user-message .message-content");

    promptContent.textContent = prompt;

    const responseContent = messageGroup.querySelector(".ai-message .message-content");

    if (response === "Refactoring...") {
        responseContent.innerHTML = `
            <span class="ai-loading">
                <span class="ai-spinner"></span>
                Refactoring...
            </span>
        `;
    }
    else {
        responseContent.textContent = response;
    }

    conversation.appendChild(messageGroup);

    return messageGroup;
}


function updateConversationResponse(messageGroup, response, createdAt) {
    const responseContent = messageGroup.querySelector(".ai-message .message-content");

    if (responseContent) {
        responseContent.textContent = response;
    }

    const messageDate = messageGroup.querySelector(".message-date");

    if (messageDate && createdAt) {
        messageDate.textContent = formatDate(createdAt);
    }
}


function addSessionToSidebar(sessionId, title) {
    const sidebarContent = document.querySelector(".sidebar-content");

    if (!sidebarContent || !sessionId || !title)
        return;

    const existingSession = [...sidebarContent.querySelectorAll(".sidebar-item")]
        .find(item =>
            item.dataset.sessionId === sessionId
        );

    if (existingSession) {
        setActiveSession(existingSession);
        return;
    }

    sidebarContent.querySelectorAll(".sidebar-item.active").forEach(item => {
        item.classList.remove("active");
    });

    const sessionItem = document.createElement("a");

    sessionItem.className = "sidebar-item active";
    sessionItem.dataset.sessionId = sessionId;
    sessionItem.textContent = title;
    sessionItem.href = `/Home/Index?sessionId=${sessionId}`;

    const newSessionButton = sidebarContent.querySelector(".new-session-button");

    if (newSessionButton) {
        newSessionButton.after(sessionItem);
    }
    else {
        sidebarContent.appendChild(sessionItem);
    }
}


function setActiveSession(sessionItem) {
    if (!sessionItem)
        return;

    document.querySelectorAll(".sidebar-item.active").forEach(item => {
        item.classList.remove("active");
    });

    sessionItem.classList.add("active");
}


function formatDate(date) {
    return new Date(date).toLocaleString([], {
        day: "2-digit",
        month: "short",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    });
}


function setRefactorButtonState(button, isLoading) {
    button.disabled = isLoading;
    button.textContent = isLoading ? "Refactoring..." : "Refactor";
}


function toggleSidebar() {
    const sidebar = document.getElementById("sidebar");

    if (sidebar) {
        sidebar.classList.toggle("open");
    }
}


function startNewSession() {
    currentSessionId = null;

    replaceURLParam(null);

    const conversation = document.getElementById("conversation");

    const codeInput = document.getElementById("code-input");

    document
        .querySelectorAll(".sidebar-item.active")
        .forEach(item => {
            item.classList.remove("active");
        });

    if (conversation) {
        conversation.innerHTML = "";
    }

    if (codeInput) {
        codeInput.value = "";
        codeInput.focus();
    }
}


function replaceURLParam(sessionId) {
    const url = new URL(window.location.href);

    if (sessionId) {
        url.searchParams.set("sessionId", sessionId);
    }
    else {
        url.searchParams.delete("sessionId");
    }

    window.history.replaceState({}, "", url);
}