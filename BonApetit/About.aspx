<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BonApetit.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <div class="col-md-8">
        <div class="panel panel-default">
            <div class="panel-heading">
                Tips
            </div>
            <ol id="cooking-tips-list" class="list-group">
                <li class="list-group-item">Remember, y'all, it’s all about the prep. Take away the stress by doing the prep the night or day before. You'll look like a star.</li>
                <li class="list-group-item">The smaller the item, the higher the baking temperature. For example, I bake mini chocolate chip-toffee cookies at 500 degrees F for only 4 minutes. Perfect end result.</li>
                <li class="list-group-item">Store spices in a cool, dark place, not above your stove. Humidity, light and heat will cause herbs and spices to lose their flavor.</li>
                <li class="list-group-item">Use a coarse microplane to shave vegetables into salads or vinaigrettes. You can create an orange-fennel dressing by adding grated fennel and orange zest to a simple vinaigrette.</li>
                <li class="list-group-item">Always make stock in a large quantity and freeze it in plastic bags. That way, when you want to make a nice soup or boil veggies, you can simply pull the bag out of the freezer.</li>
                <li class="list-group-item">If you're cooking for someone important — whether it's your boss or a date — never try a new recipe and a new ingredient at the same time.</li>
                <li class="list-group-item">Cook pasta 1 minute less than the package instructions and cook it the rest of the way in the pan with sauce.</li>
                <li class="list-group-item">After making eggs sunny-side up, deglaze the pan with sherry vinegar, then drizzle the sauce on the eggs to add another dimension to the dish.</li>
                <li class="list-group-item">After working with garlic, rub your hands vigorously on your stainless steel sink for 30 seconds before washing them. It will remove the odor.</li>
                <li class="list-group-item">Brine, baby, brine! Ya gotta brine that poultry to really give it the super flavor.</li>
            </ol>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-default">
            <div class="panel-heading">
                Gloassary
            </div>
            <div class="panel-body">
                <dl id="glossary-list" class="dl-horizontal">
                    <dt>t. or tsp.</dt>
                    <dd>teaspoon</dd>
                    <dt>T. or Tbsp.</dt>
                    <dd>tablespoon</dd>
                    <dt>c.</dt>
                    <dd>cup</dd>
                    <dt>pt.</dt>
                    <dd>pint</dd>
                    <dt>qt.</dt>
                    <dd>quart</dd>
                    <dt>oz.</dt>
                    <dd>ounce</dd>
                    <dt>lb.</dt>
                    <dd>pound</dd>
                    <dt>pkg.</dt>
                    <dd>package</dd>
                </dl>
            </div>
        </div>
    </div>
</div>
</asp:Content>
