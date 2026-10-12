namespace Domain.Abstractions;

// Устройства, которые дружелюбны к использованию новичками
public interface IBeginnerFriendly
{
    // оценка легкости в использовании 1–10. ≥ 6 - можно выдавать новичкам
    int BeginnerScore { get; }
}