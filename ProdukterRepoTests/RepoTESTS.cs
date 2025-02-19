using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class ProdukterRepoTests
{
    private ProdukterRepo _repo;

    [TestInitialize]
    public void Setup()
    {
        _repo = new ProdukterRepo();
    }

    [TestMethod]
    public void CreateUser_ShouldThrowException_WhenEmailIsInvalid()
    {
        // Arrange
        string invalidEmail = "invalidemail.com"; // Mangler @
        string password = "ValidPass@123";

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _repo.CreateUser(invalidEmail, password));
    }

    [TestMethod]
    public void CreateUser_ShouldThrowException_WhenPasswordIsWeak()
    {
        // Arrange
        string email = "test@example.com";
        string weakPassword = "123"; // For kort og mangler krav

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _repo.CreateUser(email, weakPassword));
    }

    [TestMethod]
    public void ValidateUser_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        // Arrange
        string email = "nonexistent@example.com";
        string password = "SomePassword@123";

        // Act
        bool result = _repo.ValidateUser(email, password);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ChangeUserPassword_ShouldReturnFalse_WhenOldPasswordIsIncorrect()
    {
        // Arrange
        string email = "test@example.com";
        string oldPassword = "WrongPass@123";
        string newPassword = "NewPass@123";

        // Act
        bool result = _repo.ChangeUserPassword(email, oldPassword, newPassword);

        // Assert
        Assert.IsFalse(result);
    }
}
