using AutoFixture;
using Automaton.Application.OrderPreparation;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Automaton.Application.Tests
{
    public class NewOrdersFinderTests
    {
        private readonly IFixture _fixture;
        private readonly NewOrdersFinder _sut;
        private readonly Mock<IOrderRepository> _orderRepository;
        public NewOrdersFinderTests()
        {
            _fixture = new Fixture();
            _orderRepository = new Mock<IOrderRepository>();
            _sut = new NewOrdersFinder(_orderRepository.Object);
        }

        public class IsDateOlderThanOneDay : NewOrdersFinderTests
        {
            [Fact]
            public void should_return_true_when_old_date_is_at_least_one_day_older_than_new_date()
            {
                var oldDate = new DateTime(2023, 01, 01, 10, 00, 00);
                var newDate = new DateTime(2023, 01, 02, 10, 00, 01);

                var result = _sut.IsDateOlderThanOneDay(oldDate, newDate);

                Assert.True(result);
            }

            [Fact]
            public void should_return_false_when_old_date_is_exactly_one_day_older_than_new_date()
            {
                var oldDate = new DateTime(2023, 01, 01, 10, 00, 00);
                var newDate = new DateTime(2023, 01, 02, 10, 00, 00);

                var result = _sut.IsDateOlderThanOneDay(oldDate, newDate);

                Assert.False(result);
            }

            [Fact]
            public void should_return_false_when_old_date_is_equal_new_date()
            {
                var oldDate = new DateTime(2023, 01, 01, 10, 00, 00);
                var newDate = new DateTime(2023, 01, 01, 10, 00, 00);

                var result = _sut.IsDateOlderThanOneDay(oldDate, newDate);

                Assert.False(result);
            }

            [Fact]
            public void should_return_false_when_old_date_is_no_older_than_one_day()
            {
                var oldDate = new DateTime(2023, 01, 01, 10, 00, 00);
                var newDate = new DateTime(2023, 01, 01, 10, 00, 00);

                var result = _sut.IsDateOlderThanOneDay(oldDate, newDate);

                Assert.False(result);
            }
        }

        public class FindNewOrders : NewOrdersFinderTests
        {
            [Fact]
            public void when_2_orders_are_added_in_one_operation_should_return_first_order()
            {
                var email = "test@test.pl";
                var productId = 1;
                var orders = GenerateOrders(2);

                orders[0].CustomerEmailAddress = email;
                orders[0].ProductId = productId;
                orders[0].CreatedDate = new DateTime(2023, 01, 01, 10, 00, 00);

                orders[1].CustomerEmailAddress = email;
                orders[1].ProductId = productId;
                orders[1].CreatedDate = new DateTime(2023, 01, 01, 10, 15, 00);


                var result = _sut.FindNewOrders(orders);

                Assert.NotEmpty(result);
                Assert.Single(result);
                Assert.Equal(orders.ElementAt(0).Id, result.ElementAt(0).Id);
                Assert.Equal(orders.ElementAt(0).CreatedDate, result.ElementAt(0).CreatedDate);
                Assert.Equal(orders.ElementAt(0).EmailMessageId, result.ElementAt(0).EmailMessageId);
            }

            [Fact]
            public void when_3_orders_are_added_in_one_operation_should_return_first_order()
            {
                var email = "test@test.pl";
                var productId = 1;
                var orders = GenerateOrders(3);

                orders[0].CustomerEmailAddress = email;
                orders[0].ProductId = productId;
                orders[0].CreatedDate = new DateTime(2023, 01, 01, 10, 30, 00);

                orders[1].CustomerEmailAddress = email;
                orders[1].ProductId = productId;
                orders[1].CreatedDate = new DateTime(2023, 01, 01, 10, 15, 00);

                orders[2].CustomerEmailAddress = email;
                orders[2].ProductId = productId;
                orders[2].CreatedDate = new DateTime(2023, 01, 01, 10, 00, 00);



                var result = _sut.FindNewOrders(orders);

                Assert.NotEmpty(result);
                Assert.Single(result);
                Assert.Equal(orders.ElementAt(2).Id, result.ElementAt(0).Id);
                Assert.Equal(orders.ElementAt(2).CreatedDate, result.ElementAt(0).CreatedDate);
                Assert.Equal(orders.ElementAt(2).EmailMessageId, result.ElementAt(0).EmailMessageId);
            }

            [Fact]
            public void when_order_is_added_but_there_is_same_order_not_older_than_one_day_then_should_return_empty_array()
            {
                var email = "test@test.pl";
                var productId = 1;
                var oldOrderOnDatabase = _fixture.Create<Order>();
                var newOrder = _fixture.Create<Order>();

                oldOrderOnDatabase.CustomerEmailAddress = email;
                oldOrderOnDatabase.ProductId = productId;
                oldOrderOnDatabase.CreatedDate = new DateTime(2023, 01, 01, 10, 00, 00);

                newOrder.CustomerEmailAddress = email;
                newOrder.ProductId = productId;
                newOrder.CreatedDate = new DateTime(2023, 01, 02, 10, 00, 00);

                

                _orderRepository.Setup(x => x.GetLatestSameOrder(It.IsAny<Order>())).Returns(oldOrderOnDatabase);


                var result = _sut.FindNewOrders(new List<Order> { newOrder });

                Assert.Empty(result);
            }

            [Fact]
            public void when_order_is_added_but_there_is_same_order_older_than_one_day_then_should_return_added_order()
            {
                var email = "test@test.pl";
                var productId = 1;
                var oldOrderOnDatabase = _fixture.Create<Order>();
                var newOrder = _fixture.Create<Order>();

                oldOrderOnDatabase.CustomerEmailAddress = email;
                oldOrderOnDatabase.ProductId = productId;
                oldOrderOnDatabase.CreatedDate = new DateTime(2023, 01, 01, 10, 00, 00);

                newOrder.CustomerEmailAddress = email;
                newOrder.ProductId = productId;
                newOrder.CreatedDate = new DateTime(2023, 01, 02, 10, 00, 01);

                _orderRepository.Setup(x => x.GetLatestSameOrder(It.IsAny<Order>())).Returns(oldOrderOnDatabase);


                var result = _sut.FindNewOrders(new List<Order> { newOrder });

                Assert.NotEmpty(result);
                Assert.Single(result);
                Assert.Equal(newOrder.Id, result.ElementAt(0).Id);
                Assert.Equal(newOrder.CreatedDate, result.ElementAt(0).CreatedDate);
                Assert.Equal(newOrder.EmailMessageId, result.ElementAt(0).EmailMessageId);
            }

            [Fact]
            public void when_two_orders_are_added_but_there_is_same_order_older_than_one_day_then_should_return_one_older_added_order()
            {
                var email = "test@test.pl";
                var productId = 1;
                var oldOrderOnDatabase = _fixture.Create<Order>();
                oldOrderOnDatabase.CustomerEmailAddress = email;
                oldOrderOnDatabase.ProductId = productId;
                oldOrderOnDatabase.CreatedDate = new DateTime(2023, 01, 01, 10, 00, 00);
                var orders = GenerateOrders(2);

                orders[0].CustomerEmailAddress = email;
                orders[0].ProductId = productId;
                orders[0].CreatedDate = new DateTime(2023, 01, 03, 10, 30, 00);

                orders[1].CustomerEmailAddress = email;
                orders[1].ProductId = productId;
                orders[1].CreatedDate = new DateTime(2023, 01, 03, 10, 15, 00);

                _orderRepository.Setup(x => x.GetLatestSameOrder(It.IsAny<Order>())).Returns(oldOrderOnDatabase);

                var result = _sut.FindNewOrders(orders);

                Assert.NotEmpty(result);
                Assert.Single(result);
                Assert.Equal(orders.ElementAt(1).Id, result.ElementAt(0).Id);
                Assert.Equal(orders.ElementAt(1).CreatedDate, result.ElementAt(0).CreatedDate);
                Assert.Equal(orders.ElementAt(1).EmailMessageId, result.ElementAt(0).EmailMessageId);
            }
        }

        private List<Order> GenerateOrders(int numberOfOrders)
        {
            var orders = new List<Order>();
            for(int i  =0; i < numberOfOrders; i++)
            {
                orders.Add(_fixture.Create<Order>());
            }
            return orders;
        }
    }
}