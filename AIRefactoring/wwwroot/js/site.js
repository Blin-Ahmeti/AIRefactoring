const currentPath = window.location.pathname;

document.querySelectorAll(".navbar-item").forEach(link =>
{
    var href = link.getAttribute("href");
    if (href === currentPath)
    {
        link.classList.add("active");
    } else if (href.includes("learn") && currentPath.includes("learn"))
    {
        link.classList.add("active");
    }
});